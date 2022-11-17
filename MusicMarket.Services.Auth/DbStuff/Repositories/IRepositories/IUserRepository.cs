using MusicMarket.Services.Auth.DbStuff.DbModels;
using MusicMarket.Services.Auth.Services.Base;

namespace MusicMarket.Services.Auth.DbStuff.Repositories.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>, IScopedService
    {
        Task<User> CredentialsIdentificationAsync(string email, string passwordHash);
        Task<User> GetByEmailAsync(string email);

    }
}
