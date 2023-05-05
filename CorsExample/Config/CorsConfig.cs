using Microsoft.AspNetCore.Cors.Infrastructure;

namespace CorsExample.Config
{
    public static class CorsConfig
    {
        public static Action<CorsOptions> Options
        {
            get
            {
                return options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder
                        .WithOrigins(new string[] { "http://localhost", "http://localhost:4200" })
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders(new string[] { "Content-Disposition", "WWW-Authenticate" })
                        .AllowCredentials());
                };
            }
        }
    }
}
