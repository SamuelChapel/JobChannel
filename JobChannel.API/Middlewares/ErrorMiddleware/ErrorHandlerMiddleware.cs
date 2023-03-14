using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using JobChannel.API.Middlewares.ErrorMiddleware.Responses;
using JobChannel.BLL.Exceptions;
using JobChannel.DAL.UOW;
using JobChannel.Domain.Exceptions;
using JobChannel.Domain.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JobChannel.API.Middlewares.ErrorMiddleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var _unitOfWork = httpContext.RequestServices.GetService(typeof(IUnitOfWork)) as IUnitOfWork;

            try
            {
                await _next(httpContext);

                _unitOfWork?.Commit();
            }
            catch (Exception thrownException)
            {
                await HandleExceptionAsync(httpContext, thrownException);

                _unitOfWork?.Rollback();
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception thrownException)
    {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetHttpStatusCodeByException(thrownException);

            string errorResponse = GetErrorResponseByException(thrownException);

            _logger.LogWarning(errorResponse);

            await context.Response.WriteAsync(errorResponse);
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

