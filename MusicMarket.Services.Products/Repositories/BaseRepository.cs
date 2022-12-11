using Microsoft.EntityFrameworkCore;
using MusicMarket.Services.Products.DbStuff;
using MusicMarket.Services.Products.DbStuff.DbModels;
using MusicMarket.Services.Products.Repositories.IRepositories;

namespace MusicMarket.Services.Products.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly WebContext _webContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(WebContext context)
        {
            _webContext = context;
            _dbSet = _webContext.Set<T>();
        }


        public async Task DeleteByIdAsync(long id)
        {
            _dbSet.Remove(await GetByIdAsync(id));
            await _webContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(long id)
            => await _dbSet
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<T> SaveAsync(T entity) 
        {
            if (entity.Id > 0)
            {
                _dbSet.Update(entity);
            }
            else
            {
                await _dbSet.AddAsync(entity);
            }

            await _webContext.SaveChangesAsync();
            return entity;
        }
        public async Task SaveListAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _webContext.SaveChangesAsync();   
        }

        public T Save(T entity) 
        {
            _dbSet.Add(entity);
            _webContext.SaveChanges();
            return entity;
        }
    }
}
