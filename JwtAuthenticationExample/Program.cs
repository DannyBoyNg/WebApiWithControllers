using JwtAuthenticationExample.AuthHandlers;
using JwtAuthenticationExample.Config;
using Microsoft.AspNetCore.Authorization;
using Ng.Services;

var builder = WebApplication.CreateBuilder(args);
StaticConfig.Configuration = builder.Configuration;

// Add services to the container.
//Jwt authentication
builder.Services
    .AddJwtTokenService(JwtConfig.JwtTokenSettings);
builder.Services
    .AddAuthentication(JwtConfig.AuthenticationOptions)
    .AddJwtBearer(JwtConfig.JwtBearerOptions);
//Authorization
builder.Services
    .AddAuthorization(JwtConfig.AuthorizationOptions)
    .AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
//Controllers
builder.Services
    .AddControllers();

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
