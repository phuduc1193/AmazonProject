using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Interfaces;
using Model;

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

                        return DataReaderMapToList<Product>(reader);
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

                        return DataReaderMapToObject<Product>(reader);
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
                    using (var reader = command.ExecuteReader())
                    {
                       
                    }
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

                        var results = DataReaderMapToList<Product>(reader);
                        return results;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T DataReaderMapToObject<T>(IDataReader dr)
        {
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
            }
            return obj;
        }

        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!ColumnExists(dr, prop.Name))
                        continue;
                    if (!Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
