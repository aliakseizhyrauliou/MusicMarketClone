using MusicMarket.Services.Auth.DbStuff.DbModels.Enums;

namespace MusicMarket.Services.Auth.DtoModels
{
    public class TokenDtoModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public Roles UserRoles { get; set; }
    }
}
