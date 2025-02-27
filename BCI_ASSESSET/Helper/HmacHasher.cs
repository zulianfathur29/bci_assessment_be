using System.Security.Cryptography;
using System.Text;

namespace BCI_ASSESSET.Helper
{
    public class HmacHasher
    {
        private readonly string _secretKey;

        public HmacHasher(IConfiguration configuration)
        {
            _secretKey = configuration["HmacSettings:SecretKey"];
        }

        public string HashPassword(string password)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey)))
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hash = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var hashOfInput = HashPassword(enteredPassword);
            return hashOfInput == storedHash;
        }
    }
}
