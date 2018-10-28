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

        public CredentialSchema Register(string username, string password)
        {
            var existedUser = _userProvider.GetUserCredentialByUsername(username);
            if (existedUser != null)
                throw new DuplicateUsernameException(username);

            var credentialSchema = TokenManager.Encode(username);
            var userCredential = new UserCredential
            {
                Username = username,
                Password = PasswordHasher.Hash(password),
                AccessToken = credentialSchema.AccessToken,
                RefreshToken = credentialSchema.RefreshToken
            };
            _userProvider.CreateUserCredential(userCredential);
            return credentialSchema;
        }

        public bool VerifyToken(string token)
        {
            return TokenManager.Verify(token);
        }
    }
}
