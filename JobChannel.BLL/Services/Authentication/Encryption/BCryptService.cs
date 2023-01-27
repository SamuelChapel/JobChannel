using BC = BCrypt.Net.BCrypt;

namespace JobChannel.BLL.Services.Authentication.Encryption
{
    public class BCryptService : IEncryptionService
    {
        public string HashPassword(string plainPassword)
        {
            //var salt = BCrypt.Net.BCrypt.GenerateSalt(12);

            //return BCrypt.HashPassword(plainPassword, salt, true, HashType.SHA512);
            return BC.EnhancedHashPassword(plainPassword);
        }

        public bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            return BC.EnhancedVerify(plainPassword, hashedPassword);
        }
    }
}
