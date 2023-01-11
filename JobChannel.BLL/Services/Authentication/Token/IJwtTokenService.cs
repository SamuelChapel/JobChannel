using System.Collections.Generic;

namespace JobChannel.BLL.Services.Authentication.Token
{
    public interface IJwtTokenService
    {
        string GetToken(string username, params string[] roles);
        bool IsTokenValid(string key, string issuer, string token);
    }
}