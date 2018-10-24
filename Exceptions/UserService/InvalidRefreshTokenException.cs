using System;

namespace Exceptions.UserService
{
    public class InvalidRefreshTokenException : Exception
    {
        public InvalidRefreshTokenException()
        {
        }

        public InvalidRefreshTokenException(string token)
            : base(ModifyMessage(token))
        {
        }

        private static string ModifyMessage(string token)
        {
            return $"Refresh token ({token}) is not found!";
        }

        public InvalidRefreshTokenException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
