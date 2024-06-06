using Dapper;
using Microsoft.Data.SqlClient;

namespace WebApplication2.Models
{
    public class productRepository
    {
        public void update_price(product p)
        {
            string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string query = "UPDATE product SET name = @n, price = @p WHERE Id = @i";

                connection.Open();
                try
                {
                    connection.Execute(query, new { n = p.name, i = p.Id, p = p.price });
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, e.g., logging
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
