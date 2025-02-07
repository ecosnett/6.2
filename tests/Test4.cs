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

            var insert_result = LogDataInsert.InsertLog(timestamp, message, url);

            Assert.AreEqual(Task.CompletedTask, insert_result);

            var delete_result = LogDataDelete.DeleteLog(timestamp, url);

            Assert.AreEqual(Task.CompletedTask, delete_result);
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
            DateTime timestamp = new DateTime(1111, 01, 01, 01, 01, 01);
            string url = "www.TestUrl.com";
            string message = "Test entry";
       
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await LogDataInsert.InsertLog(timestamp, message, url);
            });
        }
    }
}
