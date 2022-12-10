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
    }
}
