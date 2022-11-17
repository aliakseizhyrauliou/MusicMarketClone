using MusicMarket.Services.Auth.Services.Base;

namespace MusicMarket.Services.Auth.Services.IServices
{
    public interface IPasswordHashingService : IScopedService
    {
        string GetHashOfPassword(string password, byte[] salt);
        byte[] GenerateSalt();
    }
}
