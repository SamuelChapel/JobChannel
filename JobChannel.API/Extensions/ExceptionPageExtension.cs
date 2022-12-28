using JobChannel.API.Middlewares.ErrorMiddleware;
using Microsoft.AspNetCore.Builder;

namespace JobChannel.API.Extensions
{
    public static class ExceptionPageExtension
    {
        public static IApplicationBuilder UseExceptionPage(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }
    }
}

