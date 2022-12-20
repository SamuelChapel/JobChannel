using JobChannel.API.Middlewares.ErrorMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JobChannel.API.Extensions
{
    public static class ExceptionPageExtension
    {
        public static IApplicationBuilder UseExceptionPage(this IApplicationBuilder app, IHostEnvironment environment)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }
    }
}

