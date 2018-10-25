using Model;

namespace Interfaces
{
    public interface IUserService
    {
        CredentialSchema Login(string username, string password);
        CredentialSchema LoginWithRefreshToken(string token);
        bool VerifyToken(string token);
        CredentialSchema Register(string username, string password);
    }
}
