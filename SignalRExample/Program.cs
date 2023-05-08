using SignalRExample.Config;
using SignalRExample.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR(SignalRConfig.Options);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapHub<ProgressHub>("/Progress");
app.MapControllers();
app.Run();
