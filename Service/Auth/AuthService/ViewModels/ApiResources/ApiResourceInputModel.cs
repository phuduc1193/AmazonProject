using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class ApiResourceInputModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Secret { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public List<ScopeInputModel> Scopes { get; set; }
        public List<ClaimInputModel> Claims { get; set; }
    }
}
