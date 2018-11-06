using AuthService.Common.Interfaces.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Common.Models
{
    public class ApplicationRole : IdentityRole, IApplicationRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
