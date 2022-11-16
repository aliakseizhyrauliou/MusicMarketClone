using MusicMarket.Services.Auth.DbStuff.DbModels;
using MusicMarket.Services.Auth.DbStuff.Repositories.Attributes;

namespace MusicMarket.Services.Auth.DbStuff.Repositories.IRepositories
{
    [Repository]
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> CredentialsIdentificationAsync(string email, string passwordHash);
        Task<User> GetByEmailAsync(string email);

    }
}
