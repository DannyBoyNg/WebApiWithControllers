using CorsExample.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(CorsConfig.Options);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
