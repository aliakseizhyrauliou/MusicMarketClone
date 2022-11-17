using MusicMarket.Services.Auth.DbStuff.DbModels;
using MusicMarket.Services.Auth.DtoModels;
using MusicMarket.Services.Auth.Services.Base;
using System.Security.Claims;

namespace MusicMarket.Services.Auth.Services.IServices
{
    public interface ITokenService : IScopedService
    {

        TokenDtoModel GenerateTokens(User candidateForTokens);
        IEnumerable<Claim> GetUserClaims(User candidateForTokens);
        string GenerateAccessToken(IEnumerable<Claim> userClaims);
        string GenerateRefreshToken(IEnumerable<Claim> userClaims);
        bool ValidateRefreshToken(string refreshToken);

    }
}
