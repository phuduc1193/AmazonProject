using Interfaces;
using Model;
using ProviderUtilities;
using System.Data;
using System.Data.SqlClient;
using System;

namespace UserProviders
{
    public class UserProvider : IUserProvider
    {
        private string _connectionString;

        private UserProvider() { }

        public UserProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserCredential GetUserCredentialByUsername(string username)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(UserStoredProcedures.GetUserCredentialByUsername, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return null;

                        return ProviderUtility.DataReaderMapToObject<UserCredential>(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserCredential GetUserCredentialByRefreshToken(string token)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(UserStoredProcedures.GetUserCredentialByRefreshToken, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RefreshToken", token);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return null;

                        return ProviderUtility.DataReaderMapToObject<UserCredential>(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateTokenByUsername(string username, CredentialSchema credentialSchema)
        {
            if (credentialSchema == null) return;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(UserStoredProcedures.UpdateUserCredentialByUsername, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@AccessToken", credentialSchema.AccessToken);
                    command.Parameters.AddWithValue("@RefreshToken", credentialSchema.RefreshToken);

                    connection.Open();
                    if (command.ExecuteNonQuery() == 0)
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateUserCredential(UserCredential userCredential)
        {
            if (userCredential == null) return;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(UserStoredProcedures.InsertUserCredential, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", userCredential.Username);
                    command.Parameters.AddWithValue("@Password", userCredential.Password);
                    command.Parameters.AddWithValue("@AccessToken", userCredential.AccessToken);
                    command.Parameters.AddWithValue("@RefreshToken", userCredential.RefreshToken);

                    connection.Open();
                    var id = Convert.ToInt32(command.ExecuteScalar());
                    if (id <= 0)
                        throw new Exception("Cannot insert UserCredential");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
