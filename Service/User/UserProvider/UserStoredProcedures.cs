namespace UserProviders
{
    internal class UserStoredProcedures
    {
        public static string GetUserCredentialByUsername => "spGetUserCredentialByUsername";

        public static string GetUserCredentialByRefreshToken => "spGetUserCredentialByRefreshToken";

        public static string UpdateUserCredentialByUsername => "spUpdateUserCredentialByUsername";

        public static string InsertUserCredential => "spInsertUserCredential";
    }
}
