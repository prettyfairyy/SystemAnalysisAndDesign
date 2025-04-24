using System;
using System.Security.Cryptography;
using System.Text;

namespace CarRentalSystem.Helpers
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16; // 128 bit
        private const int HashSize = 32; // 256 bit
        private const int Iterations = 10000;

        public static string GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string saltString)
        {
            byte[] salt = Convert.FromBase64String(saltString);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            string computedHash = HashPassword(password, storedSalt);
            return storedHash == computedHash;
        }
    }
}
