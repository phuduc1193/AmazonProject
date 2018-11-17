using PaymentService.Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PaymentService.DAL
{
    public class BankDA
    {
        private string _connectionStr;

        public BankDA()
        {
            _connectionStr = "Data Source=SOL-PC;Initial Catalog=BankDB;Integrated Security=True";
        }

        public CreditCard GetAccount(string AccountNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                try
                {
                    var creditCard = new CreditCard();
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetAccount", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    command.Parameters.Add("@AccountNumber", System.Data.SqlDbType.VarChar).Value = AccountNumber;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                creditCard.AccountNumber = reader["AccountNumber"].ToString();
                                creditCard.CardHolderName = reader["CardHolderName"].ToString();
                                creditCard.CVV = reader["CVV"].ToString();
                                creditCard.ExpireDate = reader["ExpirationDate"].ToString();
                                creditCard.Balance = (double)reader["Balance"];
                            }
                        }
                        return creditCard;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return null;
                }

            }
        }
    }
}
