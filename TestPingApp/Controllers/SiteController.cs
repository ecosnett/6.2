using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPingApp.Models;
using TestPingApp.Services.sites;

namespace TestPingApp.Controllers
{
    public class SiteController : Controller
    {
        private readonly SiteDataRetriever _dataRetriever;

        public SiteController(SiteDataRetriever dataRetriever)
        {
            _dataRetriever = dataRetriever;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
<<<<<<< HEAD
            string command = "SELECT name, url FROM sites";

            var sites = await _dataRetriever.GetDataFromDatabaseAsync(command);
=======
            string command = "SELECT * FROM sites";

            var sites = await _dataRetriever.GetDataFromDatabaseAsync(command);

>>>>>>> azure/DEVELOPMENT

            if (!string.IsNullOrEmpty(searchQuery))
            {
                sites = sites.Where(site => site.Name.Contains(searchQuery)).ToList();
            }

            return View(sites);
        }
    }
}