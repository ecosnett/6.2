using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using TestPingApp.Models;

namespace TestPingApp.Services.logs
{
    public class LogDataRetriever
    {
        private readonly string _connectionString = "Data Source=D:\\Users\\edward\\codes\\C#\\PingWebApp\\sites.db";

        public async Task<List<LogDataModel>> GetDataFromDatabaseAsync()
        {
            var data = new List<LogDataModel>();

            using (var conn = new SQLiteConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SQLiteCommand("SELECT timestamp, url, message FROM site_logs", conn))
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
            return data;
        }
    }
}
