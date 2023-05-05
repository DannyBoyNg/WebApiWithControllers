using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorHandlerExample.Config
{
    public static class ErrorHandlerConfig
    {
        public static Action<IApplicationBuilder> Configuration
        {
            get
            {
                return builder =>
                {
                    builder.Run(async context =>
                    {
                        //Get the exception that was thrown
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = exceptionHandlerPathFeature?.Error;
                        var endpoint = exceptionHandlerPathFeature?.Endpoint;
                        //Determine status code based on the exception that was thrown
                        var statusCode = exception switch
                        {
                            //UserNotAuthorizedException x => StatusCodes.Status401Unauthorized,
                            //UserNotAuthenticatedException x => StatusCodes.Status401Unauthorized,
                            //...
                            _ => StatusCodes.Status500InternalServerError,
                        };
                        //Object to return
                        var pd = new ProblemDetails
                        {
                            Title = "An error has occured",
                            Detail = exception?.Message,
                            Status = statusCode,
                            Type = exception?.GetType().ToString(),
                        };
                        pd.Extensions.Add("endpoint", endpoint?.DisplayName);
                        pd.Extensions.Add("requestId", context.TraceIdentifier);
                        //Format and send response to client
                        context.Response.StatusCode = statusCode;
                        await context.Response.WriteAsJsonAsync(pd);
                    });
                };
            }
        }
    }
}
