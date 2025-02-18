using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Xml.Linq;
using TestPingApp.Models;
using TestPingApp.Services.logs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace tests
{
    [TestClass]
    public class LogInserterTests
    {
        [TestMethod]
        public async Task InsertData_Correct()
        {
            DateTime timestamp = DateTime.Now;
            string message = "Test message";
            string url = "www.Test.com";

            await LogDataInsert.InsertLog(timestamp, message, url);

            await LogDataDelete.DeleteLog(timestamp, url);
        }

        [TestMethod]
        public async Task InsertData_NoValueAsync()
        {
            DateTime timestamp = DateTime.Now;
            string message = "";
            string url = " ";

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await LogDataInsert.InsertLog(timestamp, message, url);
            });
        }

        [TestMethod]
        public async Task InsertData_DoubleRecordAsync()
        {
            DateTime timestamp = new DateTime(1753, 01, 01, 00, 00, 00);
            string url = "www.TestUrl.com";
            string message = "Test entry";

            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await LogDataInsert.InsertLog(timestamp, message, url);
            });
        }
    }
}