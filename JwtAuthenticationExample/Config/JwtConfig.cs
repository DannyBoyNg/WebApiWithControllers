using JwtAuthenticationExample.AuthRequirements;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Ng.Services;
using System.Text;

namespace JwtAuthenticationExample.Config
{
    public static class JwtConfig
    {
        public static TokenValidationParameters TokenValidationParameters
        {
            get
            {
                return new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = StaticConfig.Configuration?["JwtSettings:Issuer"], //Get settings from appsettings.json
                    ValidateAudience = true,
                    ValidAudience = StaticConfig.Configuration?["JwtSettings:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticConfig.Configuration?["JwtSettings:Key"] ?? throw new Exception("JwtSettings:Key not defined in appsettings.json"))),
                    ValidateLifetime = true,
                    SaveSigninToken = true,
                };
            }
        }

        public static Action<JwtTokenSettings> JwtTokenSettings
        {
            get
            {
                return options =>
                {
                    options.SecurityAlgorithm = SecurityAlgorithm.HS256;
                    options.AccessTokenExpirationInMinutes = int.Parse(StaticConfig.Configuration?["JwtSettings:AccessTokenExpirationInMinutes"] ?? throw new Exception("JwtSettings:AccessTokenExpirationInMinutes not defined in appsettings.json")); //Default: 60
                    options.RefreshTokenExpirationInHours = int.Parse(StaticConfig.Configuration?["JwtSettings:RefreshTokenExpirationInHours"] ?? throw new Exception("JwtSettings:RefreshTokenExpirationInHours not defined in appsettings.json")); //Default: 2
                    options.TokenValidationParameters = TokenValidationParameters;
                };
            }
        }

        public static Action<AuthenticationOptions> AuthenticationOptions
        {
            get
            {
                return options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                };
            }
        }

        public static Action<JwtBearerOptions> JwtBearerOptions
        {
            get
            {
                return options => options.TokenValidationParameters = TokenValidationParameters;
            }
        }

        public static Action<AuthorizationOptions> AuthorizationOptions
        {
            get
            {
                //https://learn.microsoft.com/en-us/aspnet/core/security/authorization/claims
                //https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies
                return options =>
                {
                    //Using just claims
                    options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber")); //Policy passed as long as the EmployeeNumber claim is present on the token
                    options.AddPolicy("Founders", policy => policy.RequireClaim("EmployeeNumber", "1", "2", "3", "4", "5")); //Policy passed as long as EmployeeNumber is 1, 2, 3, 4 or 5
                    //Using requirements
                    options.AddPolicy("AtLeast21", policy => policy.Requirements.Add(new MinimumAgeRequirement(21))); //You need to implement a handler to handle the requirement. A handler may handle multiple requirements. Don't forget to register the handlers with the DI container as a singleton in Program.cs
                    //Using assertions
                    options.AddPolicy("BadgeEntry", policy => policy.RequireAssertion(context => context.User.HasClaim(c => c.Type == "BadgeId" || c.Type == "TemporaryBadgeId"))); //Policy passed if assertion evaluates to true
                };
            }
        }

    }
}
