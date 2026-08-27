using FileNest.API.Models;
using FileNest.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileNest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MongoDbController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public MongoDbController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestConnection()
        {
            var isConnected = await _mongoDbService.TestConnectionAsync();

            if (isConnected)
            {
                return Ok(new
                {
                    message = "MongoDB connection successful!"
                });
            }

            return StatusCode(500, new
            {
                message = "MongoDB connection failed."
            });
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertTestDocument(MongoTestDocument document)
        {
            await _mongoDbService.InsertTestDocumentAsync(document);

            return Ok(new
            {
                message = "Document inserted successfully!"
            });
        }
    }
}