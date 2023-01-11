using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using JobChannel.BLL.Exceptions;
using JobChannel.BLL.Services.Authentication.Encryption;
using JobChannel.BLL.Services.Authentication.Token;
using Microsoft.Extensions.Configuration;

namespace JobChannel.BLL.Services.Authentication
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;

        public AuthenticateService(
            IEncryptionService encryptionService,
            IJwtTokenService jwtTokenService,
            IConfiguration configuration
            )
        {
            _encryptionService = encryptionService;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration;
        }

        public string Authenticate(string username, string password)
        {
            var role = VerifyUserCanAuthenticate(username, password);

            return _jwtTokenService.GetToken(username, role);
        }

        private string VerifyUserCanAuthenticate(string username, string password)
        {
            var hashedPassword = _encryptionService.HashPassword(password);

            string? role = null;

            if (username == _configuration["Users:Desktop:Login"] && hashedPassword == _configuration["Users:Desktop:Password"])
                role = _configuration["Users:Desktop:Role"];
            else if (username == _configuration["Users:Mobile:Login"] && hashedPassword == _configuration["Users:Mobile:Password"])
                role = _configuration["Users:Mobile:Role"];

            return role ?? throw new UnauthorizedException("Utilisateur non valide");
        }

    }
}
