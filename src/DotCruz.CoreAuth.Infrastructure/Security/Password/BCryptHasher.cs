using System.Security.Cryptography;
using System.Text;
using DotCruz.CoreAuth.Domain.Interfaces.Security;
using BC = BCrypt.Net.BCrypt;

namespace DotCruz.CoreAuth.Infrastructure.Security.Password
{
    public class BCryptHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            var sha256Hash = ComputeSha256(password);

            return BC.HashPassword(sha256Hash);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var sha256Hash = ComputeSha256(password);

            return BC.Verify(sha256Hash, passwordHash);
        }

        private static string ComputeSha256(string input)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));

            return Convert.ToHexString(bytes);
        }
    }
}