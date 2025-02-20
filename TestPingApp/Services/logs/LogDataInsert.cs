using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TestPingApp.Services.logs
{
    public class LogDataInsert
    {
        private static readonly string _connectionString = $"Server={Config.Server};Database={Config.Database};User Id={Config.Username};Password={Config.Password};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public async static Task InsertLog(DateTime timestamp, string message, string url, string type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(url))
                {
                    throw new ArgumentException("The url or message cannot be empty.");
                }

                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var command = new SqlCommand("INSERT INTO logs (timestamp, url, message, type) VALUES (@timestamp, @url, @message, @type)", conn))
                    {
                        command.Parameters.AddWithValue("@timestamp", timestamp);
                        command.Parameters.AddWithValue("@url", url);
                        command.Parameters.AddWithValue("@message", message);
                        command.Parameters.AddWithValue("@type", type);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                Console.WriteLine($"Error: Duplicate record with timestamp {timestamp}. Cannot insert the same timestamp again.");
                throw new Exception("Log data insertion failed due to duplicate timestamp.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while inserting log data: {ex.Message}");
                throw;
            }
        }
    }
}