using Microsoft.EntityFrameworkCore;
using MusicMarket.Services.Auth.DbStuff.DbModels;
using MusicMarket.Services.Auth.DbStuff.Repositories.IRepositories;

namespace MusicMarket.Services.Auth.DbStuff.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected WebContext _dbContex;
        protected DbSet<T> _dbSet;
        public BaseRepository(WebContext dbContex)
        {
            _dbContex = dbContex;
            _dbSet = _dbContex.Set<T>();
        }

        public async Task<bool> AnyAsync()
        {
            return await _dbSet.AnyAsync();
        }


        public bool Any()
        {
            return _dbSet.Any();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async void Remove(T dbModel)
        {
            _dbSet.Remove(dbModel);
            await _dbContex.SaveChangesAsync();
        }

        public async Task RemoveAsync(long id)
        {
            _dbSet.Remove(await GetByIdAsync(id));
            await _dbContex.SaveChangesAsync();
        }

        public async Task SaveAsync(T dbModel)
        {
            if (dbModel.Id > 0)
            {
                _dbSet.Update(dbModel);
            }
            else
            {
                _dbSet.Add(dbModel);
            }

            await _dbContex.SaveChangesAsync();
        }

        public void SaveList(IEnumerable<T> dbModelsList)
        {
            dbModelsList.ToList().ForEach(async x => await SaveAsync(x));
        }
    }
}
