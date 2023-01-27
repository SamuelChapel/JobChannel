using System.Security.Cryptography;
using System.Text;

namespace JobChannel.BLL.Services.Authentication.Encryption
{
    public class SHA256Service : IEncryptionService
    {
        public string HashPassword(string plainPassword)
        {
            using SHA256 sha256Hash = SHA256.Create();

            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            return HashPassword(plainPassword).Equals(hashedPassword);
        }
    }
}
