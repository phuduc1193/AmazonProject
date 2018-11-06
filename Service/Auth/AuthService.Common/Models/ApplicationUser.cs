using Microsoft.AspNetCore.Identity;
using System.Text;

namespace AuthService.Common.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
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
        public string ExternalProvider { get; set; }
        public string ExternalProviderId { get; set; }
    }
}
