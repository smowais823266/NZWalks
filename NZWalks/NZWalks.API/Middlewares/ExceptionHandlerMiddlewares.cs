using System.Net;
using Microsoft.OpenApi.MicrosoftExtensions;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddlewares
    {
        private readonly ILogger<ExceptionHandlerMiddlewares> iLogger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddlewares(ILogger<ExceptionHandlerMiddlewares> iLogger, RequestDelegate next)
        {
            this.iLogger = iLogger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorID = Guid.NewGuid();
                //log this exception
                iLogger.LogError(ex, $"error id:{errorID}: {ex.Message}");


                //return custom error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorID,
                    ErrorMessage = "Somethig went wrong...we are looking into this matter"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }

        }
    }
}
