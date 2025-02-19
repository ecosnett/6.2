using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TestPingApp.Services.logs
{
    public class LogDataInsert
    {
<<<<<<< HEAD
        public async static Task InsertLog(DateTime timestamp, string message, string url)
        {
            string _connectionString = "Data Source=D:\\Users\\edward\\codes\\C#\\PingWebApp\\sites.db;Version=3;BusyTimeout=30000;";
=======
        private static readonly string _connectionString = $"Server={Config.Server};Database={Config.Database};User Id={Config.Username};Password={Config.Password};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
>>>>>>> azure/DEVELOPMENT

        public async static Task InsertLog(DateTime timestamp, string message, string url)
        {
            try
            {
<<<<<<< HEAD
                if (message == null || string.IsNullOrWhiteSpace(url))
                {
                    throw new ArgumentException("The url or message cannot be empty.");
                }

                using (var conn = new SQLiteConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var command = new SQLiteCommand("INSERT INTO site_logs (timestamp, url, message) VALUES (@timestamp, @url, @message)", conn))
=======
                if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(url))
                {
                    throw new ArgumentException("The url or message cannot be empty.");
                }

                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var command = new SqlCommand("INSERT INTO logs (timestamp, url, message) VALUES (@timestamp, @url, @message)", conn))
>>>>>>> azure/DEVELOPMENT
                    {
                        command.Parameters.AddWithValue("@timestamp", timestamp);
                        command.Parameters.AddWithValue("@url", url);
                        command.Parameters.AddWithValue("@message", message);

                        await command.ExecuteNonQueryAsync();
                    }

                    
                }
            }
<<<<<<< HEAD
            catch (SQLiteException ex) when (ex.Message.Contains("UNIQUE constraint failed"))
            {
                
                Console.WriteLine($"Error: Duplicate record with timestamp {timestamp}. Cannot insert the same timestamp again.");
                throw new Exception("Log data insertion failed due to duplicate timestamp.", ex);
            }

=======
            catch (SqlException ex) when (ex.Number == 2627)
            {
                Console.WriteLine($"Error: Duplicate record with timestamp {timestamp}. Cannot insert the same timestamp again.");
                throw new Exception("Log data insertion failed due to duplicate timestamp.", ex);
            }
>>>>>>> azure/DEVELOPMENT
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while inserting log data: {ex.Message}");
                throw;
            }
        }
    }
}