using MusicMarket.Services.Auth.Attributes;
using MusicMarket.Services.Auth.DbStuff.DbModels;
using MusicMarket.Services.Auth.DtoModels;
using System.Security.Claims;

namespace MusicMarket.Services.Auth.Services.IServices
{
    [Service]
    public interface ITokenService
    {

        TokenDtoModel GenerateTokens(User candidateForTokens);
        IEnumerable<Claim> GetUserClaims(User candidateForTokens);
        string GenerateAccessToken(IEnumerable<Claim> userClaims);
        string GenerateRefreshToken(IEnumerable<Claim> userClaims);
        bool ValidateRefreshToken(string refreshToken);

    }
}
