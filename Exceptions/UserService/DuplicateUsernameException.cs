using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions.UserService
{
    public class DuplicateUsernameException : Exception
    {
        public DuplicateUsernameException()
        {
        }

        public DuplicateUsernameException(string username)
            : base(ModifyMessage(username))
        {
        }

        private static string ModifyMessage(string username)
        {
            return $"Username {username} is already existed!";
        }

        public DuplicateUsernameException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
