using IdentityServer4.EntityFramework.Entities;

namespace AuthService.ViewModels
{
    public class ApiResourceClaimViewModel : ApiResourceClaim
    {
        public string Name { get; set; }
    }
}
