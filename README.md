# Implementation of Refresh Token in .NET Core 7

This is a sample .NET Core 7 application that demonstrates how to use [JWT](https://jwt.io/) refresh tokens for authentication and authorization.

## Getting Started

1. Clone this repository.
2. Run `dotnet restore` to install the necessary packages.
3. Run `dotnet watch run` to start the application for debugging.
3. Run `dotnet run` to start the application.

## Usage

1. Open a web browser and go to `http://localhost:5286`.
2. Click on the "Register" button to create a new user account.
3. Log in with the credentials you just created.
4. Use the API endpoints to test authentication and authorization.

## API Endpoints

The following API endpoints are available:

- `POST /login`: Sample Login Controller.
- `GET /Auth/refreshToken/{token}`: Get Refresh  and Access token.
- `POST /Auth/validateRefreshToken/{token}`: Validate Refresh Token.


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.





