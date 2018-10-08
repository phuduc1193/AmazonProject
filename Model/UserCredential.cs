namespace Model
{
    public class UserCredential
    {
        public User User { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }
    }
}
