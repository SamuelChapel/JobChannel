using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace JobChannel.API.Middlewares.ErrorMiddleware.Responses
{
    public class ErrorValidationResponse : ErrorResponse
    {
        public IEnumerable<PropertyError> Errors { get; }

        public ErrorValidationResponse(ValidationException validationException) : base(validationException) 
            => Errors = validationException.Errors.Select(failure => new PropertyError(failure.PropertyName, failure.ErrorMessage));
    }
}

