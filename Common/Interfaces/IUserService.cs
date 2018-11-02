using Common.Models;

namespace Common.Interfaces
{
    public interface IUserService
    {
        CredentialSchema Login(string userName, string password);
        CredentialSchema LoginWithRefreshToken(string token);
        bool VerifyToken(string token);
        CredentialSchema Register(string userName, string password);
    }
}
