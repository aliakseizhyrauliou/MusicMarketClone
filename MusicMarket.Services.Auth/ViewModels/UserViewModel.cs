using MusicMarket.Services.Auth.DbStuff.DbModels.Enums;

namespace MusicMarket.Services.Auth.ViewModels
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public Roles Roles { get; set; }
    }
}
