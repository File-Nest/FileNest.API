using Microsoft.AspNetCore.Mvc;

namespace FileNest.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : Controller
    { 

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "API Working..."
            });
        }
    }
}
