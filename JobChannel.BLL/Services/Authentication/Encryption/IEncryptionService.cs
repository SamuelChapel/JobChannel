namespace JobChannel.BLL.Services.Authentication.Encryption
{
    public interface IEncryptionService
    {
        string HashPassword(string plainPassword);
    }
}
