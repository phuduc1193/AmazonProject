using Exceptions.UserService;
using Interfaces;
using Model;

namespace UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserProvider _userProvider;

        public UserService(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public CredentialSchema Login(string username, string password)
        {
            var userCredential = _userProvider.GetUserCredentialByUsername(username);
            if (userCredential == null)
                throw new InvalidLoginUserException(username);

            if (!PasswordHasher.Validate(password, userCredential.Password))
                throw new InvalidLoginCredentialException(userCredential.Username);
            
            var credentialSchema = TokenManager.Encode(userCredential.Username);
            _userProvider.UpdateTokenByUsername(userCredential.Username, credentialSchema);
            return credentialSchema;
        }

        public CredentialSchema LoginWithRefreshToken(string token)
        {
            var userCredential = _userProvider.GetUserCredentialByRefreshToken(token);
            if (userCredential == null)
                throw new InvalidRefreshTokenException(token);

            var credentialSchema = TokenManager.Encode(userCredential.Username);
            _userProvider.UpdateTokenByUsername(userCredential.Username, credentialSchema);
            return credentialSchema;
        }

        public bool VerifyToken(string token)
        {
            return TokenManager.Verify(token);
        }
    }
}
