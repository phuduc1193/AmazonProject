using Jose;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserServices
{
    public static class TokenManager
    {
        private const string _iss = "iss";
        private const string _secretKey = "c7cDmp";
        private static byte[] _secretKeyInBytes { get { return Encoding.ASCII.GetBytes(_secretKey); } }
        private const JwsAlgorithm _alg = JwsAlgorithm.HS256;

        public static CredentialSchema Encode(string username)
        {
            var refreshToken = Guid.NewGuid().ToString();
            GenerateIatExp(out int iat, out int exp);
            return new CredentialSchema
            {
                AccessToken = Encode(username, iat, exp),
                ExpiresIn = exp,
                TokenType = "bearer",
                RefreshToken = refreshToken
            };
        }

        public static bool Verify(string token)
        {
            try
            {
                var result = JWT.Decode(token, _secretKeyInBytes, _alg);
                return !string.IsNullOrWhiteSpace(result);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static Dictionary<string, object> GetPayloadDictionary(string username, int iat, int exp)
        {
            return new Dictionary<string, object>()
            {
                { "iss", _iss },
                { "sub", username },
                { "exp", exp },
                { "iat", iat }
            };
        }

        private static string Encode(string username, int iat, int exp)
        {
            var payload = GetPayloadDictionary(username, iat, exp);
            return JWT.Encode(payload, _secretKeyInBytes, _alg);
        }

        private static void GenerateIatExp(out int iat, out int exp)
        {
            var expiresInMinute = 120;

            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.Now;

            iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            exp = (int)issueTime.AddMinutes(expiresInMinute).Subtract(utc0).TotalSeconds;
        }
    }
}
