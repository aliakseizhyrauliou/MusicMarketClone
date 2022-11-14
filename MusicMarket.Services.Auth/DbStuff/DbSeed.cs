using MusicMarket.Services.Auth.DbStuff.DbModels;

namespace MusicMarket.Services.Auth.DbStuff
{
    public class DbSeed : IDbSeed
    {
        private WebContext _webContext;
        public DbSeed(WebContext webContext)
        {
            _webContext = webContext;   
        }
        public void Initialize()
        {
            if (!_webContext.Users.Any()) 
            {
                var userAdmin = new User()
                {
                    FirstName = "Alex",
                    LastName = "Admin",
                    Email = "Admin",
                    Role = Enums.Role.Admin
                };

                _webContext.Users.Add(userAdmin);
                _webContext.SaveChanges();
            }
        }
    }
}
