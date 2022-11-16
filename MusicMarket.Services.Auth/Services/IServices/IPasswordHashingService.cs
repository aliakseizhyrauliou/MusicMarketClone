using MusicMarket.Services.Auth.Attributes;

namespace MusicMarket.Services.Auth.Services.IServices
{
    [Service]
    public interface IPasswordHashingService
    {
        string GetHashOfPassword(string password, byte[] salt);
        byte[] GenerateSalt();
    }
}
