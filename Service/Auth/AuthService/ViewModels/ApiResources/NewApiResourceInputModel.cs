using System.Collections.Generic;

namespace AuthService.ViewModels
{
    public class NewApiResourceInputModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public string ScopeName { get; set; }
        public string ScopeDisplayName { get; set; }
        public string ScopeDescription { get; set; }
        public bool ScopeEmphasize { get; set; }
        public bool ScopeRequired { get; set; }

        public string SerializedScopes { get; set; }
    }
}
