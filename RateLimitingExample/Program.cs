using RateLimitingExample.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRateLimiter(RateLimiterConfig.Options);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
