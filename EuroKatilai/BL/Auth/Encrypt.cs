using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BL.Auth
{
    public class Encrypt : IEncrypt
    {
        public string HashPassword(string password, string salt)
        {

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                 password,
                 System.Text.Encoding.ASCII.GetBytes(password),
                 KeyDerivationPrf.HMACSHA512,
                 5000,
                 64
                 ));
        }
    }
}
