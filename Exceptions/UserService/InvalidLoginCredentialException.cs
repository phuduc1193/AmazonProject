using System;

namespace Exceptions.UserService
{
    public class InvalidLoginCredentialException : Exception
    {
        public InvalidLoginCredentialException()
        {
        }

        public InvalidLoginCredentialException(string username)
            : base(ModifyMessage(username))
        {
        }

        private static string ModifyMessage(string username)
        {
            return $"Invalid login credential for user {username}!";
        }

        public InvalidLoginCredentialException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
