using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace UserServices
{
    internal static class PasswordHasher
    {
        private const string _salt = "4Et2wU";
        private const KeyDerivationPrf _keyDerivationPrf = KeyDerivationPrf.HMACSHA1;
        private const int _iterationCount = 10000;
        private const int _numBytesRequested = 256 / 8;

        public static string Hash(this string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            var hashedPasswordInBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(_salt),
                prf: _keyDerivationPrf,
                iterationCount: _iterationCount,
                numBytesRequested: _numBytesRequested
            );

            return Convert.ToBase64String(hashedPasswordInBytes);
        }

        public static bool Validate(string value, string hashedValue)
        {
            return Hash(value) == hashedValue;
        }
    }
}
