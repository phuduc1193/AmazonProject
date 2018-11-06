using AuthService.Common.Models;
using System.Collections.Generic;

namespace AuthService.Common.Interfaces.Models
{
    public interface IApplicationRole
    {
        string Name { get; set; }
        string NormalizedName { get; set; }
        string ConcurrencyStamp { get; set; }

        ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
