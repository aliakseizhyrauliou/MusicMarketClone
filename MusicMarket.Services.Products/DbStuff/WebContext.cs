using Microsoft.EntityFrameworkCore;
using MusicMarket.Services.Products.DbStuff.DbModels;

namespace MusicMarket.Services.Products.DbStuff
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Product> Products { get; set; } 
        public DbSet<Category> Categories { get; set; }
    }
}
