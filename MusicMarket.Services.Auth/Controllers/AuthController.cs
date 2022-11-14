using Microsoft.AspNetCore.Mvc;
using MusicMarket.Services.Auth.DbStuff;
using MusicMarket.Services.Auth.DbStuff.DbModels;

namespace MusicMarket.Services.Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private WebContext _webContext;
        public AuthController(WebContext webContext)
        {
            _webContext = webContext;   
        }

        [HttpGet]
        public IEnumerable<User> GetUsers() 
        {
            var users = _webContext.Users.ToList();
            return users;
        }
    }

}