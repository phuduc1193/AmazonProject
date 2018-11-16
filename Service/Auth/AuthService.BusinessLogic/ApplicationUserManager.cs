using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthService.Common.Interfaces.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuthService.BusinessLogic
{
    public class ApplicationUserManager<TUser> : UserManager<TUser>
        where TUser : class, IApplicationUser
    {
        public ApplicationUserManager(IUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators, IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        { }

        public async Task<IList<Claim>> GetJwtClaimsAsync(TUser user)
        {
            var results = await base.GetClaimsAsync(user) as List<Claim>;
            if (results == null)
                results = new List<Claim>();

            results.Add(new Claim(JwtClaimTypes.Subject, user.Id));
            results.AddRange(GetClaimsFromUserData(user));

            var roles = await base.GetRolesAsync(user) as List<string>;
            foreach (var role in roles)
            {
                results.Add(new Claim(JwtClaimTypes.Role, role));
            }

            return results;
        }

        private static IList<Claim> GetClaimsFromUserData(TUser user)
        {
            var results = new List<Claim>();

            if (!string.IsNullOrWhiteSpace(user.Email))
                results.Add(new Claim(JwtClaimTypes.Email, user.Email));

            if (user.EmailConfirmed)
                results.Add(new Claim(JwtClaimTypes.EmailVerified, "true"));
            else results.Add(new Claim(JwtClaimTypes.EmailVerified, "false"));

            if (!string.IsNullOrWhiteSpace(user.FirstName))
                results.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

            if (!string.IsNullOrWhiteSpace(user.LastName))
                results.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));

            if (!string.IsNullOrWhiteSpace(user.MiddleName))
                results.Add(new Claim(JwtClaimTypes.MiddleName, user.MiddleName));

            if (!string.IsNullOrWhiteSpace(user.FullName))
                results.Add(new Claim(JwtClaimTypes.Name, user.FullName));

            if (!string.IsNullOrWhiteSpace(user.UserName))
                results.Add(new Claim(JwtClaimTypes.PreferredUserName, user.UserName));

            return results;
        }
    }
}
