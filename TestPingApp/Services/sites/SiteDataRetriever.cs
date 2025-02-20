using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TestPingApp.Models;

namespace TestPingApp.Services.sites
{
    public class SiteDataRetriever
    {
        private readonly string _connectionString = $"Server={Config.Server};Database={Config.Database};User Id={Config.Username};Password={Config.Password};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly IMemoryCache _cache;

        public SiteDataRetriever(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<List<SiteDataModel>> GetDataFromDatabaseAsync(string command)
        {
            if (_cache.TryGetValue("site_data", out List<SiteDataModel>? cachedData))
            {
                return cachedData!;
            }

            var data = new List<SiteDataModel>();

            try
            {
                if (string.IsNullOrWhiteSpace(command))
                {
                    throw new ArgumentException("The command cannot be empty or whitespace.");
                }

                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(command, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            data.Add(new SiteDataModel
                            {
                                Url = reader.GetString(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                    conn.Close();
                }

                if (data.Count == 0)
                {
                    throw new KeyNotFoundException("No records found in the database.");
                }
                _cache.Set("site_data", data, TimeSpan.FromMinutes(10));
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