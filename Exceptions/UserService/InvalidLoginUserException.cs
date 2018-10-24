using System;

namespace Exceptions.UserService
{
    public class InvalidLoginUserException : Exception
    {
        public InvalidLoginUserException()
        {
        }

        public InvalidLoginUserException(string username)
            : base(ModifyMessage(username))
        {
        }

        private static string ModifyMessage(string username)
        {
            return $"User {username} is not found!";
        }

        public InvalidLoginUserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
