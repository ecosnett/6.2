using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Xml.Linq;
using TestPingApp.Models;
using TestPingApp.Services.sites;
using Microsoft.Extensions.Caching.Memory;  

namespace tests
{
    [TestClass]
    public class SiteRecieverTests
    {
        private readonly SiteDataRetriever _dataRetriever;

        public SiteRecieverTests() 
        {
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions()); 
            _dataRetriever = new SiteDataRetriever(memoryCache); 
        }

        [TestMethod]
        public async Task Test_GetSiteDataFromDatabaseAsync_ReturnData()
        {

            string command = "SELECT * FROM sites where name = 'google'";

            var sites = await _dataRetriever.GetDataFromDatabaseAsync(command);

            Assert.IsNotNull(sites);
            Assert.IsTrue(sites.Count > 0);
            Assert.IsTrue(sites[0].Name.Contains("google"));
            Assert.IsTrue(sites[0].Url.Contains("www.google.com"));
        }

        [TestMethod]
        public async Task Test_GetDataFromDatabaseAsync_EmptyCommand()
        {
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions()); 
            var dataRetriever = new SiteDataRetriever(memoryCache);

            string command = "";
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await dataRetriever.GetDataFromDatabaseAsync(command);
            });

        }

        [TestMethod]
        public async Task Test_GetDataFromDatabaseAsync_NoRecord()
        {
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions()); 
            var dataRetriever = new SiteDataRetriever(memoryCache);

            string command = "SELECT name, url FROM sites WHERE name = 'fakename'";

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () =>
            {
                await dataRetriever.GetDataFromDatabaseAsync(command);
            });
        }
    }
}
