using System;

namespace Model
{
    public class UserCredential
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
