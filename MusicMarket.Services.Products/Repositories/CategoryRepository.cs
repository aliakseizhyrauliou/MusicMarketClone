using Microsoft.EntityFrameworkCore;
using MusicMarket.Services.Products.DbStuff;
using MusicMarket.Services.Products.DbStuff.DbModels;
using MusicMarket.Services.Products.Repositories.IRepositories;

namespace MusicMarket.Services.Products.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(WebContext context) : base(context)
        {
        }

        public Task<bool> IsExistAsync(Category category)
        {
            return _dbSet
                .AnyAsync(x => x.Id == category.Id && x.Name == category.Name);
        }
    }
}
