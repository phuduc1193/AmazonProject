using System.Threading.Tasks;

namespace AuthService.Services
{
    public interface ILoginService<TUser>
    {
        Task<bool> ValidateCredentialsAsync(TUser user, string password);
        Task<TUser> FindByUsernameAsync(string userName);
        Task SignInAsync(TUser user, bool isPersistent);
        Task SignOutAsync();
        Task<TUser> FindUserByExternalProviderAsync(string provider, string providerUserId);
    }
}
