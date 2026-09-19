using FileNest.Data.Entities;
using FileNest.Model.Models;
using FileNest.Service;
using Microsoft.AspNetCore.Mvc;

namespace FileNest.Web.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequestModel user)
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