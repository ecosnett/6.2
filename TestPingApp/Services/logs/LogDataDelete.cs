<<<<<<< HEAD
﻿using System.Data.SQLite;
=======
﻿using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
>>>>>>> azure/DEVELOPMENT

namespace TestPingApp.Services.logs
{
    public class LogDataDelete
    {
<<<<<<< HEAD
        public async static Task DeleteLog(DateTime timestamp, string url)
        {
            string _connectionString = "Data Source=D:\\Users\\edward\\codes\\C#\\PingWebApp\\sites.db;Version=3;BusyTimeout=30000;";

            try
            {
                using (var conn = new SQLiteConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (var command = new SQLiteCommand("DELETE FROM site_logs WHERE url = @url AND timestamp = @timestamp", conn))
=======
        private static readonly string _connectionString = $"Server={Config.Server};Database={Config.Database};User Id={Config.Username};Password={Config.Password};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public async static Task DeleteLog(DateTime timestamp, string url)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (var command = new SqlCommand("DELETE FROM logs WHERE url = @url AND timestamp = @timestamp", conn))
>>>>>>> azure/DEVELOPMENT
                    {
                        command.Parameters.AddWithValue("@url", url);
                        command.Parameters.AddWithValue("@timestamp", timestamp);

<<<<<<< HEAD
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
=======
                        await command.ExecuteNonQueryAsync();
                    }
>>>>>>> azure/DEVELOPMENT
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
