using Microsoft.AspNetCore.Mvc;

namespace ProcessEngine.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {
        
        private readonly ILogger<InfoController> _logger;

        public InfoController(ILogger<InfoController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Info")]
        public IActionResult Get()
        {
            return Ok("ProcessEngine.API is running");
        }
    }
}
