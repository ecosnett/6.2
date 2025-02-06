using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Xml.Linq;
using TestPingApp.Models;
using TestPingApp.Services.sites;

namespace tests
{
    [TestClass]
    public class SiteRecieverTests
    {
        private readonly SiteDataRetriever _dataRetriever;

        public SiteRecieverTests()
        {
            _dataRetriever = new SiteDataRetriever();
        }

        [TestMethod]
        public async Task Test_GetDataFromDatabaseAsync_ReturnData()
        {
            var dataRetriever = new SiteDataRetriever();
            string command = "SELECT name, url FROM sites where name = 'google'";

            List<SiteDataModel> site = await dataRetriever.GetDataFromDatabaseAsync(command);

            Assert.IsNotNull(site);
            Assert.IsTrue(site.Count > 0);
            Assert.IsTrue(site[0].Name.Contains("google"));
            Assert.IsTrue(site[0].Url.Contains("www.google.com"));
        }

        [TestMethod]
        public async Task Test_GetDataFromDatabaseAsync_EmptyCommand()
        {
            var dataRetriever = new SiteDataRetriever();
            string command = "";
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await dataRetriever.GetDataFromDatabaseAsync(command);
            });

        }

        [TestMethod]
        public async Task Test_GetDataFromDatabaseAsync_NoRecord()
        {
            var dataRetriever = new SiteDataRetriever();
            string command = "SELECT name, url FROM sites WHERE name = 'fakename'";

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () =>
            {
                await dataRetriever.GetDataFromDatabaseAsync(command);
            });
        }
    }
}
