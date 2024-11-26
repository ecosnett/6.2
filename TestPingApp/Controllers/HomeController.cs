using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourNamespace.Models;
using YourNamespace.Services;

namespace YourNamespace.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataRetriever _dataRetriever;

        public HomeController(DataRetriever dataRetriever)
        {
            _dataRetriever = dataRetriever;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            // Retrieve data from the database
            var sites = await _dataRetriever.GetDataFromDatabaseAsync();

            // Filter based on search query if provided
            if (!string.IsNullOrEmpty(searchQuery))
            {
                sites = sites.Where(site => site.Name.Contains(searchQuery)).ToList();
            }

            return View(sites); // Pass filtered data to the view
        }
    }
}

