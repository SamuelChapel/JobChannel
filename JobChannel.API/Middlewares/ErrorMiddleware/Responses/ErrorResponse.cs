using System;

namespace JobChannel.API.Middlewares.ErrorMiddleware.Responses
{
    public class ErrorResponse
    {
        public string ExceptionType { get; }
        public string Message { get; }

        public ErrorResponse(Exception exception)
        {
            ExceptionType = exception.GetType().FullName!;
            Message = exception.ToString();
        }
    }
}

