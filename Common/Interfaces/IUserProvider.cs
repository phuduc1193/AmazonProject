using Common.Models;

namespace Common.Interfaces
{
    public interface IUserProvider
    {
        UserCredential GetUserCredentialByUsername(string userName);
        UserCredential GetUserCredentialByRefreshToken(string token);
        void UpdateTokenByUsername(string userName, CredentialSchema credentialSchema);
        void CreateUserCredential(UserCredential userCredential);
    }
}
