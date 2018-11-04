using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthService.ViewModels
{
    public class NewApiResourceViewModel
    {
        public string Name { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string SerializedScopes { get; set; }

        public List<NewApiResourceScopeViewModel> Scopes { get; set; }
    }
}
