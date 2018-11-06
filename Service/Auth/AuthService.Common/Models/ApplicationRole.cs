using AuthService.Common.Interfaces.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
