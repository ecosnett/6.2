using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public class DataRetriever
    {
        private readonly string _connectionString;

        // Constructor allows for Dependency Injection of the connection string
        public DataRetriever(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Retrieves data asynchronously from the SQLite database
        public async Task<List<MyDataModel>> GetDataFromDatabaseAsync()
        {
            var data = new List<MyDataModel>();

            // Using a try-catch block for improved error handling
            try
            {
                using (var conn = new SQLiteConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    // Query to select data from the sites table
                    using (var cmd = new SQLiteCommand("SELECT id, name, url FROM sites", conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Read through the data and add to the list
                        while (await reader.ReadAsync())
                        {
                            data.Add(new MyDataModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Url = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Handle database-specific exceptions here
                // You might log the exception or return a specific error result
                // For now, we’ll just throw it so the caller knows something went wrong
                throw new ApplicationException("Error accessing the database.", ex);
            }
            catch (Exception ex)
            {
                // Catch other general exceptions and rethrow
                throw new ApplicationException("An unexpected error occurred.", ex);
            }

            return data;
        }
    }
}
