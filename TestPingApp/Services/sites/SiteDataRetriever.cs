using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using TestPingApp.Models;

namespace TestPingApp.Services.sites
{
    public class SiteDataRetriever
    {
        private readonly string _connectionString = "Data Source=D:\\Users\\edward\\codes\\C#\\PingWebApp\\sites.db;Version=3;BusyTimeout=30000;";

        public async Task<List<SiteDataModel>> GetDataFromDatabaseAsync(string command)
        {
            var data = new List<SiteDataModel>();

            try
            {
                if (string.IsNullOrWhiteSpace(command))
                {
                    throw new ArgumentException("The command cannot be empty or whitespace.");
                }

                using (var conn = new SQLiteConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SQLiteCommand(command, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            data.Add(new SiteDataModel
                            {
                                Name = reader.GetString(0),
                                Url = reader.GetString(1)
                            });
                        }
                    }
                    conn.Close();
                }

                if (data.Count == 0)
                {
                    throw new KeyNotFoundException("No records found in the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

            return data;
        }
    }
}