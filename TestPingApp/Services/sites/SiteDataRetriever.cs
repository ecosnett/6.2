using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using TestPingApp.Models;

namespace TestPingApp.Services.sites
{
    public class SiteDataRetriever
    {
        private readonly string _connectionString = "Data Source=D:\\Users\\edward\\codes\\C#\\PingWebApp\\sites.db";

        public async Task<List<SiteDataModel>> GetDataFromDatabaseAsync()
        {
            var data = new List<SiteDataModel>();

            using (var conn = new SQLiteConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SQLiteCommand("SELECT name, url FROM sites", conn))
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
            }
            return data;
        }
    }
}
