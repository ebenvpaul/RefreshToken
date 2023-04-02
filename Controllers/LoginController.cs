using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginModel login)
        {
            // Authenticate user
            var userId = await AuthenticateUserAsync(login);

            if (userId != null)
            {
                // Generate JWT token
                var token = _configuration.GenerateJwtToken(userId);

                // Return JWT token
                return Ok(token);
            }

            // Unauthorized access
            return Unauthorized();
        }

        private async Task<string> AuthenticateUserAsync(LoginModel login)
        {
            // Implement your own user authentication logic here
            // ...
        if (login != null)
            {return "12345";}
            // For demo purposes, let's assume the user is authenticated
            return "12345";
        }

    }
}