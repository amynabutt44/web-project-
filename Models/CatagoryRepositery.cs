using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplication2.Models
{
    public class CatagoryRepositery
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public int getid(string name)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID FROM catagory WHERE name = @name";
                return connection.QuerySingleOrDefault<int>(query, new { name });
            }
        }

        public void update(catagory c)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE catagory SET image = @image WHERE Id = @Id";
                connection.Execute(query, new { c.image, c.Id });
            }
        }
    }

    
}
