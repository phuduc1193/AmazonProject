using System.Collections.Generic;

namespace AuthService.ViewModels.Scopes
{
    public class ScopesViewModel
    {
        public List<string> ApiScopes { get; set; }
        public List<string> IdentityScopes { get; set; }
    }
}
