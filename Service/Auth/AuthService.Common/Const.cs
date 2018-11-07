using AuthService.Common.Interfaces.Models;

namespace AuthService.Common
{
    public class Const
    {
        public class ExternalProvider
        {
            public const string Google = "google";
        }

        public const string ConnectionString = "DefaultConnection";
        public const string MigrationAssembly = "MigrationAssembly";

        public class DefaultRoles
        {
            public const string Admin = "Admin";
            public const string Manager = "Manager";
            public const string Member = "Member";
        }

        public class DefaultUsers
        {
            public class Admin
            {
                public const string UserName = "admin";
                public const string Email = "admin@email.com";
                public const string Password = "P@$$w0rd";
            }
        }
    }
}
