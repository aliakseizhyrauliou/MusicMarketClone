using MusicMarket.Services.Auth.DbStuff.Enums;

namespace MusicMarket.Services.Auth.DbStuff.DbModels
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public string PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public Role Role { get; set; }  


    }
}
