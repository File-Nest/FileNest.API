using FileNest.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace FileNest.Web.Controllers
{
    [ApiController]
    [Route("Test")]
    public class UserController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public UserController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var result = await _mongoDbService.TestConnectionAsync();

            if (result)
            {
                return Ok("MongoDB connection successful.");
            }

            return StatusCode(500, "MongoDB connection failed.");
        }
    }
}