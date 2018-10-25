using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Interfaces;
using Model;
using ProviderUtilities;

namespace ProductProviders
{
    public class ProductProvider : IProductProvider
    {
        private string _connectionString;

        private ProductProvider() { }

        public ProductProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Product> GetList()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var commandText = "SELECT * FROM Products";
                    var command = new SqlCommand(commandText, connection);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return null;

                        return ProviderUtility.DataReaderMapToList<Product>(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var commandText = "SELECT * FROM Products WHERE Id = @Id";
                    var command = new SqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return null;

                        return ProviderUtility.DataReaderMapToObject<Product>(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int AddProduct(Product product)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(ProductStoredProcedures.Insert, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@CategoryId", product.Category?.Id);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateProduct(int id, Product product)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(ProductStoredProcedures.Update, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@CategoryId", product.Category?.Id);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProductById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var commandText = "DELETE FROM Products WHERE Id = @Id";
                    var command = new SqlCommand(commandText, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

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

        public List<Product> Search(string query)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(ProductStoredProcedures.Search, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Query", query);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return null;

                        var results = ProviderUtility.DataReaderMapToList<Product>(reader);
                        return results;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product GetFeaturedProduct()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(ProductStoredProcedures.FeaturedProduct, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return null;

                        return ProviderUtility.DataReaderMapToObject<Product>(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
