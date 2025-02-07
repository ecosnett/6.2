using System.Data.SQLite;

namespace TestPingApp.Services.logs
{
    public class LogDataDelete
    {
        public async static Task DeleteLog(DateTime timestamp, string url)
        {
            string _connectionString = "Data Source=D:\\Users\\edward\\codes\\C#\\PingWebApp\\sites.db;Version=3;BusyTimeout=30000;";

            try
            {
                using (var conn = new SQLiteConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (var command = new SQLiteCommand("DELETE FROM site_logs WHERE url = @url AND timestamp = @timestamp", conn))
                    {
                        command.Parameters.AddWithValue("@url", url);
                        command.Parameters.AddWithValue("@timestamp", timestamp);

                        command.ExecuteNonQuery();
                    }
                    conn.Close();
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
