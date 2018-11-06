namespace AuthService.Common.Interfaces.Models
{
    public interface IApplicationUser
    {
        string ExternalProvider { get; set; }
        string ExternalProviderId { get; set; }
        string FirstName { get; set; }
        string FullName { get; }
        string LastName { get; set; }
        string MiddleName { get; set; }
    }
}