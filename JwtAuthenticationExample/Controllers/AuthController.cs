using Microsoft.AspNetCore.Mvc;
using Ng.Services;
using System.Security.Claims;

namespace JwtAuthenticationExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService tokenService;

        public AuthController(JwtTokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [Route("Token")]
        public ActionResult Token(string username, string password)
        {
            //Input validation (not empty or null)
            //...

            //Get user from database
            var user = new 
            {
                UserId = 1,
                Username = "admin",
                PasswordHash = "hash..."
            };

            //Check if user exists
            //...

            //Check if user is active
            //...

            //Optional Check for too many failed attempts
            //...

            //Verify the password
            //...
            if (password != "123") return Unauthorized("Login Failed");

            //Add any desired claims to token
            var claims = new List<Claim>
            {
                new Claim("uid", user.UserId.ToString()),
            };

            //Add any desired roles to token
            var roles = new string[] { "user" };

            //Create tokens
            var accessToken = tokenService.GenerateAccessToken(username, roles, claims);
            var refreshToken = tokenService.GenerateRefreshToken();

            //Store refresh token in database
            //...

            //Optional log login success
            //...

            return Ok(new JwtToken { AccessToken = accessToken, RefreshToken = refreshToken, TokenType = "bearer" });
        }

        [Route("Refresh")]
        public ActionResult Refresh(string accessToken, string refreshToken)
        {
            //Get userId claim from access token
            var claimsPrincipal = tokenService.GetClaimsFromExpiredAccessToken(accessToken);
            var _ = tokenService.GetClaim(claimsPrincipal, "uid");

            //Check if refresh token belongs to user
            //...

            //Check if refresh token is still valid (not expired)
            if (tokenService.IsRefreshTokenExpired(refreshToken)) return BadRequest("Expired refresh token");

            //Create tokens
            var newAccessToken = tokenService.GenerateAccessTokenFromOldAccessToken(accessToken);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            //Store refresh token in database and remove old one
            //...

            //optional log refresh success
            //...

            return Ok(new JwtToken { AccessToken = newAccessToken, RefreshToken = newRefreshToken, TokenType = "bearer" });
        }
    }
}