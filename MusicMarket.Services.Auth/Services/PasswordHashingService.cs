using MusicMarket.Services.Auth.Services.IServices;
using System.Security.Cryptography;
using System.Text;

namespace MusicMarket.Services.Auth.Services
{
    public class PasswordHashingService : IPasswordHashingService
    {
        private const int maximumSaltLength = 32;

        public byte[] GenerateSalt()
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        public string GetHashOfPassword(string password, byte[] salt)
        {
            var md5 = MD5.Create();
            var hashPassword = md5.ComputeHash(Encoding.UTF8.GetBytes(password).Concat(salt).ToArray()); //Compute password and salt
            return Convert.ToBase64String(hashPassword);
        }
    }
}
