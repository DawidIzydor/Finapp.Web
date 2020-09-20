using System;
using System.Security.Cryptography;

namespace Finapp.Authentication
{
    public static class PasswordManager
    {
        private const int SaltLength = 13;
        private const int HashLength = 26;
        private const int Iterations = 120000;

        public static string GeneratePassword(string modelPassword)
        {
            var salt = CreateSaltValue();
            var hash = CreateHash(modelPassword, salt);
            var combinedHash = Combine(salt, hash);

            return Convert.ToBase64String(combinedHash);
        }

        private static byte[] Combine(byte[] salt, byte[] hash)
        {
            var hashBytes = CreateSaltArray();
            Array.Copy(salt, 0, hashBytes, 0, SaltLength);
            Array.Copy(hash, 0, hashBytes, SaltLength, HashLength);
            return hashBytes;
        }

        private static byte[] CreateSaltArray()
        {
            return new byte[SaltLength + HashLength];
        }

        private static byte[] CreateHash(string modelPassword, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(modelPassword, salt, Iterations);
            var hash = pbkdf2.GetBytes(HashLength);
            return hash;
        }

        private static byte[] CreateSaltValue()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltLength]);
            return salt;
        }

        public static bool VerifyPassword(string passwordToVerify, string passwordHash)
        {
            var modelPassBytes = Convert.FromBase64String(passwordHash);
            var salt = new byte[SaltLength];
            Array.Copy(modelPassBytes, 0, salt, 0, SaltLength);
            var hash = new Rfc2898DeriveBytes(passwordToVerify, salt, Iterations).GetBytes(HashLength);

            for (var i = 0; i < HashLength; i++)
            {
                if (modelPassBytes[i + SaltLength] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}