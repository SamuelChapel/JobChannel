using System.Net.Http;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.PoleEmploi.Auth
{
    public interface IAuthServicePoleEmploi
    {
        bool IsExpired { get; }

        public Task<string?> GenerateAccessToken(HttpClient client);
    }
}