using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class PingController : Controller
    {
        [HttpGet("PingUrl")]
        public IActionResult PingUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("URL cannot be empty.");

            try
            {
                var ping = new Ping();
                var reply = ping.Send(url);

                if (reply.Status == IPStatus.Success)
                {
                    return Json(new { success = true, message = $"Ping to {url} successful. Roundtrip time: {reply.RoundtripTime} ms" });
                }
                else
                {
                    return Json(new { success = false, message = $"Ping to {url} failed." });
                }
            }
            catch
            {
                return Json(new { success = false, message = "Ping operation failed." });
            }
        }
    }
}