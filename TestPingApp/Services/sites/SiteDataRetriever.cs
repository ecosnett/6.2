using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TestPingApp.Models;

namespace TestPingApp.Services.sites
{
    public class SiteDataRetriever
    {
<<<<<<< HEAD
        private readonly string _connectionString = "Data Source=D:\\Users\\edward\\codes\\C#\\PingWebApp\\sites.db;Version=3;BusyTimeout=30000;";
=======
        private readonly string _connectionString = $"Server={Config.Server};Database={Config.Database};User Id={Config.Username};Password={Config.Password};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
>>>>>>> azure/DEVELOPMENT

        public async Task<List<SiteDataModel>> GetDataFromDatabaseAsync(string command)
        {
            var data = new List<SiteDataModel>();

            try
            {
                if (string.IsNullOrWhiteSpace(command))
                {
                    throw new ArgumentException("The command cannot be empty or whitespace.");
                }

<<<<<<< HEAD
                using (var conn = new SQLiteConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SQLiteCommand(command, conn))
=======
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(command, conn))
>>>>>>> azure/DEVELOPMENT
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            data.Add(new SiteDataModel
                            {
<<<<<<< HEAD
                                Name = reader.GetString(0),
                                Url = reader.GetString(1)
=======
                                Url = reader.GetString(0),
                                Name = reader.GetString(1)
>>>>>>> azure/DEVELOPMENT
                            });
                        }
                    }
                    conn.Close();
                }

                if (data.Count == 0)
                {
                    throw new KeyNotFoundException("No records found in the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

            return data;
        }
    }
}