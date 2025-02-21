using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.NetworkInformation;
using TestPingApp.Controllers;

namespace tests
{
    [TestClass]
    public class PingServiceTests
    {
        [TestMethod]
        public void PingUrl_EmptyUrl_ReturnsFailure()
        {
            string type = " ";
            string emptyUrl = " ";

            var result = PingService.PingUrl(emptyUrl, type);

            Assert.AreEqual("The URL cannot be empty or whitespace.", result.Message);
        }

        [TestMethod]
        public void PingUrl_ValidUrl_SuccessfulPing()
        {
            string validUrl = "www.google.com";
            string type = "Test";

            var result = PingService.PingUrl(validUrl, type);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Message.Contains("Ping to"));
        }

        [TestMethod]
        public void PingUrl_InvalidUrl_ReturnsFailure()
        {

            string invalidUrl = "invalid.url.com";
            string type = "Test";

            var result = PingService.PingUrl(invalidUrl, type);

            Assert.IsTrue(result.Message.Contains("failed"));
        }
    }
}


