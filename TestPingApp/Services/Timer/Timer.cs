using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestPingApp.Controllers;
using TestPingApp.Services.sites;

namespace TestPingApp.Services.Timer
{
    public class TimedBackgroundService : BackgroundService
    {
        private readonly ILogger<TimedBackgroundService> _logger;
        private readonly SiteDataRetriever _siteDataRetriever;

        public TimedBackgroundService(ILogger<TimedBackgroundService> logger, SiteDataRetriever siteDataRetriever)
        {
            _logger = logger;
            _siteDataRetriever = siteDataRetriever;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await PerformTask();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task PerformTask()
        {
            _logger.LogInformation($"Executing task at: {DateTime.Now}");

            var sites = await _siteDataRetriever.GetDataFromDatabaseAsync("SELECT * FROM sites");

            foreach (var site in sites)
            {
                string url = site.Url;
                string type = "Automatic";
                PingService.PingUrl(url, type);
                _logger.LogInformation($"Pinged {url}.");
            }
        }
    }
}

