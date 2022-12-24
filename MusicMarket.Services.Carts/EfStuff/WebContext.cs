using Microsoft.EntityFrameworkCore;

namespace MusicMarket.Services.Carts.EfStuff
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions options) : base(options)
        {
        }


    }
}
