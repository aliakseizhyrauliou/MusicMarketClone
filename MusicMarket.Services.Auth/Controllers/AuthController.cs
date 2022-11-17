using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicMarket.Services.Auth.DbStuff;
using MusicMarket.Services.Auth.DbStuff.DbModels;
using MusicMarket.Services.Auth.DbStuff.DbModels.Enums;
using MusicMarket.Services.Auth.DbStuff.Repositories.IRepositories;
using MusicMarket.Services.Auth.DtoModels;
using MusicMarket.Services.Auth.Services.IServices;
using MusicMarket.Services.Auth.ViewModels;

namespace MusicMarket.Services.Auth.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        private ITokenService _tokenService;
        private IPasswordHashingService _passwordHashingService;
        private IMapper _mapper;
        public AuthController(IUserRepository userRepository,
            ITokenService tokenService, 
            IPasswordHashingService passwordHashingService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHashingService = passwordHashingService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/getUsers")]
        [Authorize]
        public async Task<IEnumerable<User>> GetUsers() 
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<ActionResult<TokenDtoModel>> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var salt = _passwordHashingService.GenerateSalt();
                var user = new User()
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.UserName,
                    FirstName = registerViewModel.FirstName,
                    Salt = salt,
                    Roles = Roles.User,
                    PasswordHash = _passwordHashingService.GetHashOfPassword(registerViewModel.Password, salt)
                };

                await _userRepository.SaveAsync(user);

                var userTokens = _tokenService.GenerateTokens(user);

                return Ok(userTokens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/login")]
        public async Task<ActionResult<TokenDtoModel>> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var candidate = await _userRepository.GetByEmailAsync(loginViewModel.Email);
                if (candidate == null)
                {
                    return NotFound();
                }

                if (_passwordHashingService.GetHashOfPassword(loginViewModel.Password, candidate.Salt) == candidate.PasswordHash)
                {
                    var userTokens = _tokenService.GenerateTokens(candidate);
                    return Ok(userTokens);
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }

}