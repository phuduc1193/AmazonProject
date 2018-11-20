using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class ClaimInputModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public bool Checked { get; set; }
    }
}
