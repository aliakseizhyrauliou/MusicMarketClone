using Microsoft.AspNetCore.Mvc;
using MusicMarket.Services.Auth.DbStuff;
using MusicMarket.Services.Auth.DbStuff.DbModels;
using MusicMarket.Services.Auth.DbStuff.Repositories.IRepositories;
using MusicMarket.Services.Auth.DtoModels;
using MusicMarket.Services.Auth.Services.IServices;
using MusicMarket.Services.Auth.ViewModels;

namespace MusicMarket.Services.Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        private ITokenService _tokenService;
        private IPasswordHashingService _passwordHashingService;
        public AuthController(IUserRepository userRepository,
            ITokenService tokenService, IPasswordHashingService passwordHashingService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHashingService = passwordHashingService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers() 
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }

        [HttpPost]
        public TokenDtoModel Registration(UserViewModel userViewModel) 
        {
            return null;
        }
    }

}