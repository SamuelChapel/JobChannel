namespace JobChannel.BLL.Services.Authentication.Encryption
{
    public interface IEncryptionService
    {
        public string HashPassword(string plainPassword);

        public bool VerifyPassword(string hashedPassword, string plainPassword);
    }
}
