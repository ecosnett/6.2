using System.Data.SQLite;

namespace TestPingApp.Services.logs
{
    public class LogDataInsert
    {
        public static void InsertLog(DateTime timestamp, string message, string url)
        {
            string _connectionString = "Data Source=D:\\Users\\edward\\codes\\C#\\PingWebApp\\sites.db";

            try
            {
                using (var conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();

                    using (var command = new SQLiteCommand("INSERT INTO site_logs (timestamp, url, message) VALUES (@timestamp, @url, @message)", conn))
                    {
                        command.Parameters.AddWithValue("@timestamp", timestamp);
                        command.Parameters.AddWithValue("@url", url);
                        command.Parameters.AddWithValue("@message", message);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while inserting log data: {ex.Message}");
                throw;
            }
        }
    }
}
