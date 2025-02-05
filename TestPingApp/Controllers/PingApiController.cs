using Microsoft.AspNetCore.Mvc;

namespace TestPingApp.Controllers
{
    [ApiController]
    [Route("api/ping")]
    public class PingApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult PingUrl([FromQuery] string url)
        {
            var result = PingService.PingUrl(url);
            return Ok(result);
        }
    }
}
