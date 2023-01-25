using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JobChannel.Tests.Integrations.Fixtures
{
    public class TestAuthUserHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthUserHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim>()
            { 
                new Claim(ClaimTypes.Name, "Test user"),
                new Claim(ClaimTypes.Role, "User")
            };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "TestSchemeUser");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}