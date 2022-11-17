using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicMarket.Services.Auth.DbStuff.DbModels;
using MusicMarket.Services.Auth.DbStuff.Repositories.IRepositories;

namespace MusicMarket.Services.Auth.DbStuff.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(WebContext dbContex) : base(dbContex)
        {
        }

        public async Task<User> CredentialsIdentificationAsync(string email, string passwordHash)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == passwordHash);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
