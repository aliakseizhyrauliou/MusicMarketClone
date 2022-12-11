using Microsoft.EntityFrameworkCore;
using MusicMarket.Services.Products.DbStuff.DbModels;
using System.Reflection.Metadata;

namespace MusicMarket.Services.Products.DbStuff
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Product)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<Product> Products { get; set; } 
        public DbSet<Category> Categories { get; set; }
    }
}
