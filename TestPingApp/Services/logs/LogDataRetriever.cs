using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TestPingApp.Models;

namespace TestPingApp.Services.logs
{
    public class LogDataRetriever
    {
        private readonly string _connectionString = $"Server={Config.Server};Database={Config.Database};User Id={Config.Username};Password={Config.Password};";

        public async Task<List<LogDataModel>> GetLogDataFromDatabaseAsync(string command)
        {
            var data = new List<LogDataModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(command))
                {
                    throw new ArgumentException("The command cannot be empty or whitespace.");
                }

                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(command, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            data.Add(new LogDataModel
                            {
                                TimeStamp = reader.GetDateTime(0),
                                Url = reader.GetString(1),
                                Message = reader.GetString(2)
                            });
                        }
                    }
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
