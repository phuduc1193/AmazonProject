namespace AuthService.Common.Interfaces.Models
{
    public interface IApplicationRole
    {
        string Name { get; set; }
        string NormalizedName { get; set; }
        string ConcurrencyStamp { get; set; }
    }
}
