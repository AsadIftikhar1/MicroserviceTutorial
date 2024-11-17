using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
            
        }
        [HttpGet]
        public string Testing()
        {
            return "Is the api working";
        }
        public class PlatformReadDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Publisher { get; set; }
            public string Cost { get; set; }
        }
        [HttpPost]
        [Route("TestInboundConnection")]
        public ActionResult TestInboundConnection([FromBody] PlatformReadDto platform)
        {
            // Log the received platform data
            Console.WriteLine($"Received Platform: {platform.Name}, Publisher: {platform.Publisher}, Cost: {platform.Cost}");

            // Return a response with the received data
            return Ok(new { message = "Inbound test from Platform Controller", platform });
        }
    }
}
