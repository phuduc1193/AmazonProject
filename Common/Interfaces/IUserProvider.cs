using Common.Models;

namespace Common.Interfaces
{
    public interface IUserProvider
    {
        UserCredential GetUserCredentialByUsername(string username);
        UserCredential GetUserCredentialByRefreshToken(string token);
        void UpdateTokenByUsername(string username, CredentialSchema credentialSchema);
        void CreateUserCredential(UserCredential userCredential);
    }
}
