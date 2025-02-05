using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.NetworkInformation;
using TestPingApp.Controllers;

[TestClass]
public class PingServiceTests
{
    [TestMethod]
    public void PingUrl_EmptyUrl_ReturnsFailure()
    {

        string emptyUrl = " ";

        var result = PingService.PingUrl(emptyUrl);

        Assert.AreEqual("The URL cannot be empty or whitespace.", result.Message);
    }

    [TestMethod]
    public void PingUrl_ValidUrl_SuccessfulPing()
    {
        
        string validUrl = "www.google.com";
        var result = PingService.PingUrl(validUrl);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Message.Contains("Ping to"));
    }

    [TestMethod]
    public void PingUrl_InvalidUrl_ReturnsFailure()
    {
  
        string invalidUrl = "invalid.url.com";

        var result = PingService.PingUrl(invalidUrl);

        Assert.IsTrue(result.Message.Contains("failed"));
    }
}

