using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class LoginInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }
}