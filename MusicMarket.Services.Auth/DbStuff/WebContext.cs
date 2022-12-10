using Microsoft.EntityFrameworkCore;
using MusicMarket.Services.Auth.DbStuff.DbModels;


namespace MusicMarket.Services.Auth.DbStuff
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
