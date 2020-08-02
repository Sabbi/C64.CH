using System;
using System.Security.Cryptography;
using System.Text;

namespace C64.Services
{
    public class Sha256PasswordHasher : IPasswordHasher
    {
        private int saltLength = 32;

        public string HashPassword(string password)
        {
            var salt = Guid.NewGuid().ToString().Replace("-", "");
            return salt + Sha256String(password + salt);
        }

        public bool VerifyHashedPassword(string password, string hashedPassword)
        {
            if (hashedPassword == null || hashedPassword.Length < (saltLength + 1))
                return false;

            var challenge = Sha256String(password + hashedPassword.Substring(0, saltLength));
            return challenge == hashedPassword.Substring(saltLength);
        }

        private string Sha256String(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}