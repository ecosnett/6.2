using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TestPingApp.Models;
using TestPingApp.Services.logs;

namespace TestPingApp.Controllers
{
    public class LogController : Controller
    {
        private readonly LogDataRetriever _dataRetriever;

        public LogController(LogDataRetriever dataRetriever)
        {
            _dataRetriever = dataRetriever;
        }

        public async Task<IActionResult> Index(string SearchQuery, string DateSearchQuery)
        {

            string command = "SELECT timestamp, url, message FROM logs";

            var logs = await _dataRetriever.GetLogDataFromDatabaseAsync(command);

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                logs = logs.Where(log => log.Url.Contains(SearchQuery)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(DateSearchQuery) && DateTime.TryParse(DateSearchQuery, out var date))
            {
                logs = logs.Where(log => log.TimeStamp.Date == date.Date).ToList();
            }

            return View("logs_page", logs);
        }
    }
}
