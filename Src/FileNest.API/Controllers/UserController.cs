using FileNest.Model.Models;
using FileNest.Service;
using Microsoft.AspNetCore.Mvc;

namespace FileNest.Web.Controllers
{
    [ApiController]
    [Route("test")]
    public class UserController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;
        private readonly UserService _userService;
        public UserController(MongoDbService mongoDbService, UserService userService)
        {
            _mongoDbService = mongoDbService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            await _mongoDbService.TestConnectionAsync();
            return Ok("MongoDB connection successful.");
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserClass user)
        {
            await _userService.CreateUserAsync(user);
            return Ok(new { message = "User created successfully." });
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }
    }
}