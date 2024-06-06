using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Data;

namespace WebApplication2.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
    {
        private readonly string connectionString;
        public GenericRepository(string c)
        {
            connectionString = c;
        }

        public void Add(TEntity entity)
        {
            // Get table name
            var tableName = typeof(TEntity).Name;

            // Get properties excluding the primary key "Id"
            var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "Id");

            // Generate column and parameter names
            var columnNames = string.Join(",", properties.Select(x => x.Name));
            var parameterNames = string.Join(",", properties.Select(y => "@" + y.Name));

            // Create the SQL query
            var query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames})";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    connection.Execute(query, entity);
                }
                catch (Exception ex)
                {
                    // Handle the exception appropriately, such as logging or throwing
                    Console.WriteLine($"Error adding {tableName}: " + ex.Message);
                }
            }
        }

        public void delete(TEntity entity)
        {
            // Get table name
            var tableName = typeof(TEntity).Name;

            // Get primary key property
            var primaryKeyProperty = typeof(TEntity).GetProperty("Id");
            if (primaryKeyProperty == null)
            {
                throw new InvalidOperationException("Entity does not contain a property named 'Id'.");
            }

            // Create the SQL query
            var query = $"DELETE FROM {tableName} WHERE Id = @Id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    connection.Execute(query, new { Id = primaryKeyProperty.GetValue(entity) });
                }
                catch (Exception ex)
                {
                    // Handle the exception appropriately, such as logging or throwing
                    Console.WriteLine($"Error deleting from {tableName}: " + ex.Message);
                }
            }
        }

        public List<TEntity> GetAll()
        {
            // Get table name
            var tableName = typeof(TEntity).Name;

            // Create the SQL query
            var query = $"SELECT * FROM {tableName}";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<TEntity>(query).ToList();
            }
        }

        public List<TEntity> GetById(int c_id)
        {
            // Get table name
            var tableName = typeof(TEntity).Name;

            // Get property name for the category ID
            var categoryIdProperty = typeof(TEntity).GetProperty("c_id");
            if (categoryIdProperty == null)
            {
                throw new InvalidOperationException("Entity does not contain a property named 'c_id'.");
            }

            // Create the SQL query
            var query = $"SELECT * FROM {tableName} WHERE c_id = @c_id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<TEntity>(query, new { c_id }).ToList();
            }
        }
    }
}
