using System.Threading.Tasks;

namespace JobChannel.BLL.Services.Authentication
{
    public interface IAuthenticateService
    {
        /// <summary>
        /// Method who return the token who permit to authenticate
        /// </summary>
        /// <param name="username">Name of user to authenticate</param>
        /// <param name="password">Password of user to authenticate</param>
        /// <returns>String representation of the Token</returns>
        public string Authenticate(string username, string password);

        /// <summary>
        /// Method to validate a token
        /// </summary>
        /// <param name="token">The token to validate</param>
        /// <returns>True if the token is valid or false if the token is invalid</returns>
        public bool ValidateAuthenticationToken(string token);
    }
}