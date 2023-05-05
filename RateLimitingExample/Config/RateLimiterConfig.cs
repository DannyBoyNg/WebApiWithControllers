using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace RateLimitingExample.Config
{
    //https://devblogs.microsoft.com/dotnet/announcing-rate-limiting-for-dotnet/
    public static class RateLimiterConfig
    {
        public static Action<RateLimiterOptions> Options
        {
            get
            {
                return options =>
                {
                    options.RejectionStatusCode = 429; //configure http status code to return
                    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                        RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                            factory: partition => new FixedWindowRateLimiterOptions
                            {
                                AutoReplenishment = true,
                                PermitLimit = 10,
                                QueueLimit = 0,
                                Window = TimeSpan.FromMinutes(1)
                            }));
                };
            }
        }
    }
}
