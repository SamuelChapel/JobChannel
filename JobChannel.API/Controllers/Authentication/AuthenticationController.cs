using System.Threading.Tasks;
using FluentValidation;
using JobChannel.API.Controllers.Security.Requests;
using JobChannel.BLL.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobChannel.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("/authenticate")]
        public async Task<string> Authenticate(
            [FromBody] AuthenticateRequest request,
            [FromServices] IValidator<AuthenticateRequest> validator,
            [FromServices] IAuthenticateService authenticationService)
        {
            await validator.ValidateAndThrowAsync(request);

            return authenticationService.Authenticate(request.Login, request.Password);
        }
    }
}
