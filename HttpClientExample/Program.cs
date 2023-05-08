using HttpClientExample.Config;
using HttpClientExample.HttpClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<MyHttpClient>(HttpClientConfig.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
