using System.Security.Cryptography;
using System.Text;

namespace ComputerpartsLibrary.SERVICE
{
    /// <summary>
    /// Secure password hashing service using PBKDF2 (Rfc2898DeriveBytes)
    /// </summary>
    public class PasswordHashService
    {
        private const int SaltSize = 16; // 128 bit
        private const int HashSize = 32; // 256 bit
        private const int Iterations = 100_000;

        public string HashPassword(string password)
        {
            var saltBytes = new byte[SaltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            var hashBytes = pbkdf2.GetBytes(HashSize);

            return $"{Convert.ToBase64String(saltBytes)}:{Convert.ToBase64String(hashBytes)}";
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
                return false;

            var parts = hashedPassword.Split(':');
            if (parts.Length != 2)
                return false;

            byte[] saltBytes;
            byte[] hashBytes;
            try
            {
                saltBytes = Convert.FromBase64String(parts[0]);
                hashBytes = Convert.FromBase64String(parts[1]);
            }
            catch
            {
                return false;
            }

            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            var computed = pbkdf2.GetBytes(hashBytes.Length);

            return CryptographicOperations.FixedTimeEquals(computed, hashBytes);
        }
    }
}
