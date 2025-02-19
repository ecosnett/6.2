using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TestPingApp.Services.logs
{
    public class LogDataDelete
    {
        private static readonly string _connectionString = $"Server={Config.Server};Database={Config.Database};User Id={Config.Username};Password={Config.Password};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public async static Task DeleteLog(DateTime timestamp, string url)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (var command = new SqlCommand("DELETE FROM logs WHERE url = @url AND timestamp = @timestamp", conn))
                    {
                        command.Parameters.AddWithValue("@url", url);
                        command.Parameters.AddWithValue("@timestamp", timestamp);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting log data: {ex.Message}");
                throw;
            }
        }
    }
}
