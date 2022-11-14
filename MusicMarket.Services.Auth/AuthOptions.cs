using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MusicMarket.Services.Auth
{
    public static class AuthOptions
    {
        public const string ISSUER = "MusicMarket.AuthService";
        public const string AUDIENCE = "MusicMarket.Services";
        const string KEY = "mysupersecret_secretkey!123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
