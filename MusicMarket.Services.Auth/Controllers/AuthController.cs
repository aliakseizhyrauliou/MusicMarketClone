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
        private ResponceDto responceDto;
        private IPasswordHashingService _passwordHashingService;
        private ITokenService _tokenService;
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public AuthController( 
            IPasswordHashingService passwordHashingService, 
            ITokenService tokenService, 
            IUserRepository userRepository,
            IMapper mapper)
        {
            this.responceDto = new ResponceDto();
            _passwordHashingService = passwordHashingService;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _mapper = mapper;
        }       


        [HttpPost]
        [Route("/register")]
        public async Task<ResponceDto> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    responceDto.ErrorMessages.Add("Invalid data");
                    responceDto.IsSuccess = false;
                    return responceDto;
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
                responceDto.Result = userTokens;

                return responceDto;
            }
            catch (Exception ex)
            {
                responceDto.ErrorMessages = new List<string>() { ex.Message };
                responceDto.IsSuccess = false;
                return responceDto;
            }
        }

        [HttpPost]
        [Route("/login")]
        public async Task<ResponceDto> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var candidate = await _userRepository.GetByEmailAsync(loginViewModel.Email);
                if (candidate == null)
                {
                    responceDto.ErrorMessages = new List<string> { "User not found" };
                    responceDto.IsSuccess = false;
                    return responceDto;
                }

                if (_passwordHashingService.GetHashOfPassword(loginViewModel.Password, candidate.Salt) == candidate.PasswordHash)
                {
                    var userTokens = _tokenService.GenerateTokens(candidate);
                    responceDto.Result = userTokens;
                    return responceDto;
                }

                responceDto.IsSuccess = false;
                return responceDto;

            }
            catch (Exception ex)
            {
                responceDto.ErrorMessages = new List<string> { ex.Message };
                responceDto.IsSuccess = false;
                return responceDto;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/refreshToken")]
        public async Task<ResponceDto> RefreshToken(string refreshToken)
        {
            if (_tokenService.ValidateRefreshToken(refreshToken))
            {
                try
                {
                    var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                    var candidate = await _userRepository.GetByIdAsync(userId);

                    var userTokens = _tokenService.GenerateTokens(candidate);
                    responceDto.Result = userTokens;
                    return responceDto;


                }
                catch (Exception ex)
                {
                    responceDto.ErrorMessages = new List<string> { ex.Message };
                    responceDto.IsSuccess = false;
                    return responceDto;
                }
            }

            responceDto.IsSuccess = false;
            responceDto.ErrorMessages = new List<string> { "Invalid refresh_token" };
            return responceDto;
        }
    }
}

