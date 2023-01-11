using FluentValidation;
using JobChannel.Domain.Contracts;

namespace JobChannel.API.Controllers.Security.Requests
{
    public record AuthenticateRequest(
        string Login,
        string Password
        ) : IRequest;

    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(r => r.Login).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}