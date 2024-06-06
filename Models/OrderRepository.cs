using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class OrderRepository
    {
        public List<Orderr> GetOrders(string userId)
        {
            List<Orderr> orders = new List<Orderr>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM orderr WHERE UserId = @UserId ORDER BY OrderDate DESC";

                try
                {
                    connection.Open();
                    orders = connection.Query<Orderr>(query, new { UserId = userId }).AsList();
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, e.g., logging
                    Console.WriteLine(ex.Message);
                }
            }

            return orders;
        }
    }
}
