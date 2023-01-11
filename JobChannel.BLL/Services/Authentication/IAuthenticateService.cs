using System.Threading.Tasks;

namespace JobChannel.BLL.Services.Authentication
{
    public interface IAuthenticateService
    {
        public string Authenticate(string username, string password);
    }
}