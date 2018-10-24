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
        private const JwsAlgorithm _alg = JwsAlgorithm.HS256;

        private static Dictionary<string, object> GetPayloadDictionary(string username, int expiredIn)
        {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.Now;

            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddMinutes(expiredIn).Subtract(utc0).TotalSeconds;

            return new Dictionary<string, object>()
            {
                { "iss", _iss },
                { "sub", username },
                { "exp", exp },
                { "iat", iat }
            };
        }

        private static string Encode(string username, int expiresInMinute)
        {
            var payload = GetPayloadDictionary(username, expiresInMinute);
            return JWT.Encode(payload, _secretKey, _alg);
        }

        public static CredentialSchema Encode(string username)
        {
            var expiresInMinute = 120;
            var refreshToken = new Guid().ToString();

            return new CredentialSchema
            {
                AccessToken = Encode(username, expiresInMinute),
                ExpiresIn = expiresInMinute,
                TokenType = "bearer",
                RefreshToken = refreshToken
            };
        }

        public static bool Verify(string token)
        {
            try
            {
                var result = JWT.Decode(token, _secretKey, _alg);
                return !string.IsNullOrWhiteSpace(result);
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
