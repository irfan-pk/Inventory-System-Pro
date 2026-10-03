using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_System_Pro
{
    internal static class PasswordHasher
    {
        private const int Iterations = 600000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public static string Hash(string password)
        {
            byte[] salt = new byte[SaltSize];

            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            byte[] hash;

            using (var deriveBytes = new Rfc2898DeriveBytes(
                Encoding.UTF8.GetBytes(password),
                salt,
                Iterations,
                HashAlgorithmName.SHA256))
            {
                hash = deriveBytes.GetBytes(HashSize);
            }

            return "v1$pbkdf2-sha256$" + Iterations + "$" +
                   Convert.ToBase64String(salt) + "$" +
                   Convert.ToBase64String(hash);
        }

        public static bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(password) ||
                string.IsNullOrWhiteSpace(storedHash))
                return false;

            try
            {
                string[] parts = storedHash.Split('$');

                if (parts.Length != 5 ||
                    parts[0] != "v1" ||
                    parts[1] != "pbkdf2-sha256" ||
                    parts[2] != Iterations.ToString())
                    return false;

                byte[] salt = Convert.FromBase64String(parts[3]);
                byte[] expectedHash = Convert.FromBase64String(parts[4]);

                if (salt.Length != SaltSize || expectedHash.Length != HashSize)
                    return false;

                byte[] actualHash;

                using (var deriveBytes = new Rfc2898DeriveBytes(
                    Encoding.UTF8.GetBytes(password),
                    salt,
                    Iterations,
                    HashAlgorithmName.SHA256))
                {
                    actualHash = deriveBytes.GetBytes(HashSize);
                }

                return FixedTimeEquals(actualHash, expectedHash);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left.Length != right.Length)
                return false;

            int difference = 0;

            for (int i = 0; i < left.Length; i++)
                difference |= left[i] ^ right[i];

            return difference == 0;
        }
    }
}
