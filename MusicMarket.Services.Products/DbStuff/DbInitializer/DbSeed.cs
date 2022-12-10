using MusicMarket.Services.Products.DbStuff.DbModels;

namespace MusicMarket.Services.Products.DbStuff.DbInitializer
{
    public class DbSeed : IDbSeed
    {
        private WebContext _dbContext;

        public DbSeed(WebContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            if (!_dbContext.Categories.Any() &&
                    !_dbContext.Products.Any())
            {
                var cat1 = new Category()
                {
                    Name = "String"
                };

                var cat2 = new Category()
                {
                    Name = "Drums"
                };

                _dbContext.Categories.AddRange(new List<Category> { cat1, cat2 });

                var product1 = new Product()
                {
                    Category = cat1,
                    Name = "Gitar"
                };

                var product2 = new Product()
                {
                    Category = cat1,
                    Name = "Snare"
                };

                _dbContext.AddRange(new List<Product> { product1, product2 });

                _dbContext.SaveChanges();
            }
        }
    }
}
