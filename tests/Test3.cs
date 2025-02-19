using Microsoft.SqlServer.Server;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Xml.Linq;
using TestPingApp.Models;
using TestPingApp.Services.logs;
using TestPingApp.Services.sites;
using static System.Net.Mime.MediaTypeNames;

namespace tests
{
    [TestClass]
    public class LogReceiverTests
    {
        private readonly LogDataRetriever _dataRetriever;

        public LogReceiverTests()
        {
            _dataRetriever = new LogDataRetriever();
        }

        [TestMethod]
        public async Task Test_GetLogDataFromDatabaseAsync_ReturnData()
        {
<<<<<<< HEAD
            //DateTime timestamp = new DateTime(1111, 01, 01, 01, 01, 01);
            //string url = "www.TestUrl.com";
            //string message = "Test entry";
            //LogDataInsert.InsertLog(timestamp, message, url);

            var dataRetriever = new LogDataRetriever();
            string command = "SELECT timestamp, url, message FROM site_logs WHERE timestamp = '1111-01-01 01:01:01'";
=======
            DateTime timestamp = new DateTime(1753, 01, 01, 00, 00, 00);
            string url = "www.TestUrl.com";
            string message = "Test entry";
            LogDataInsert.InsertLog(timestamp, message, url);

            var dataRetriever = new LogDataRetriever();
            string command = "SELECT timestamp, url, message FROM logs WHERE timestamp = '1753-01-01 00:00:00'";
>>>>>>> azure/DEVELOPMENT

            List<LogDataModel> log = await dataRetriever.GetLogDataFromDatabaseAsync(command);

            Assert.IsNotNull(log);
            Assert.IsTrue(log.Count > 0);
            Assert.IsTrue(log[0].Url.Contains("www.TestUrl.com"));
            Assert.IsTrue(log[0].Message.Contains("Test entry"));
        }
        [TestMethod]
        public async Task Test_GetLogDataFromDatabaseAsync_EmptyCommand()
        {
            var dataRetriever = new LogDataRetriever();
            string command = "";
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await dataRetriever.GetLogDataFromDatabaseAsync(command);
            });
        }
        [TestMethod]
        public async Task Test_GetLogDataFromDatabaseAsync_NoRecord()
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