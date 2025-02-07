using System.Data.SQLite;

namespace TestPingApp.Services.logs
{
    public class LogDataInsert
    {
        public async static Task InsertLog(DateTime timestamp, string message, string url)
        {
            string _connectionString = "Data Source=D:\\Users\\edward\\codes\\C#\\PingWebApp\\sites.db;Version=3;BusyTimeout=30000;";

            try
            {
                if (message == null || string.IsNullOrWhiteSpace(url))
                {
                    throw new ArgumentException("The url or message cannot be empty.");
                }

                using (var conn = new SQLiteConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var command = new SQLiteCommand("INSERT INTO site_logs (timestamp, url, message) VALUES (@timestamp, @url, @message)", conn))
                    {
                        command.Parameters.AddWithValue("@timestamp", timestamp);
                        command.Parameters.AddWithValue("@url", url);
                        command.Parameters.AddWithValue("@message", message);

                        await command.ExecuteNonQueryAsync();
                    }

                    
                }
            }
            catch (SQLiteException ex) when (ex.Message.Contains("UNIQUE constraint failed"))
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