using MusicMarket.Services.Products.DbStuff;
using MusicMarket.Services.Products.DbStuff.DbModels;
using MusicMarket.Services.Products.Repositories.IRepositories;

namespace MusicMarket.Services.Products.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(WebContext context) : base(context)
        {
        }
    }
}
