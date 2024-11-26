using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public class DataRetriever
    {
        private readonly string _connectionString = "Data Source=D:\\Users\\edward\\codes\\C#\\azure-repo\\sites.db; Version=3;";

        public async Task<List<MyDataModel>> GetDataFromDatabaseAsync()
        {
            var data = new List<MyDataModel>();

            using (var conn = new SQLiteConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SQLiteCommand("SELECT id, name, url FROM sites", conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        data.Add(new MyDataModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Url = reader.GetString(2)
                        });
                    }
                }
            }
            return data;
        }
    }
}
