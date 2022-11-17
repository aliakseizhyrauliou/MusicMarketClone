using MusicMarket.Services.Auth.Services.Base;

namespace MusicMarket.Services.Auth.DbStuff
{
    public interface IDbSeed : IScopedService
    {
        void Initialize();
    }
}
