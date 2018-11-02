using Microsoft.AspNetCore.Identity;
using System.Text;

namespace AuthService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string MiddleName { get; internal set; }
        public string FullName {
            get
            {
                var sb = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(FirstName))
                    sb.Append($"{FirstName} ");
                if (!string.IsNullOrWhiteSpace(LastName))
                    sb.Append($"{LastName} ");
                if (!string.IsNullOrWhiteSpace(MiddleName))
                    sb.Append($"{MiddleName.Substring(0,1)}.");
                return sb.ToString().Trim();
            }
        }
        public string ExternalProvider { get; internal set; }
        public string ExternalProviderId { get; internal set; }
    }
}
