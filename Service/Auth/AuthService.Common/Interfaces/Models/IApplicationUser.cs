using AuthService.Common.Models;
using System.Collections.Generic;

namespace AuthService.Common.Interfaces.Models
{
    public interface IApplicationUser
    {
        string UserName { get; set; }
        string Email { get; set; }
        string ExternalProvider { get; set; }
        string ExternalProviderId { get; set; }
        string FirstName { get; set; }
        string FullName { get; }
        string LastName { get; set; }
        string MiddleName { get; set; }

        ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}