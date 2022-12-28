using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using JobChannel.API.Middlewares.ErrorMiddleware.Responses;
using JobChannel.BLL.Exceptions;
using JobChannel.Domain.Exceptions.Base;
using Microsoft.AspNetCore.Http;

namespace JobChannel.API.Middlewares.ErrorMiddleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next) 
            => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception thrownException)
            {
                await HandleExceptionAsync(httpContext, thrownException);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception thrownException)
    {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetHttpStatusCodeByException(thrownException);

            await context.Response.WriteAsync(GetErrorResponseByException(thrownException));
        }

        private string GetErrorResponseByException(Exception thrownException)
        {
            return thrownException switch
            {
                ValidationException validationException => JsonSerializer.Serialize(new ErrorValidationResponse(validationException)),
                _ => JsonSerializer.Serialize(new ErrorResponse(thrownException))
            };
        }

        private HttpStatusCode GetHttpStatusCodeByException(Exception thrownException)
        {
            return thrownException switch
            {
                BadRequestException or ValidationException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                UnauthorizedException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}

