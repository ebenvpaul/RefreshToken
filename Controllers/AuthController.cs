using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("refreshToken/{token}")]
        public IActionResult RefreshToken(string token)
        {
            var tokenStrings = token.Split("|");

            var accessToken = tokenStrings[0];
            var refreshToken = tokenStrings[1];
             return Ok(new
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            });
        }
        [HttpPost("validateRefreshToken/{refreshToken}")]
        public IActionResult ValidateRefreshToken(string refreshToken)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);
                var handler = new JwtSecurityTokenHandler();

                var claimsPrincipal = handler.ValidateToken(refreshToken,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return BadRequest("Invalid token");
                }

                if (claimsPrincipal is not ClaimsPrincipal claims || !claims.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
                {
                    return BadRequest("Invalid token");
                }

                var userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

                // Generate a new access token and return it to the client
                var newAccessToken = _configuration.GenerateJwtToken(userId);

                return Ok(newAccessToken);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid token : "+ ex.Message);
            }
        }
    }
}
