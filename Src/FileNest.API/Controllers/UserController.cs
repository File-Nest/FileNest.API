using AutoMapper;
using FileNest.Model.Models;
using FileNest.Service;
using FileNest.Web.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FileNest.Web.Controllers
{
    [ApiController]
    [Route("test")]
    public class UserController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        public UserController(MongoDbService mongoDbService, UserService userService, IMapper mapper)
        {
            _mongoDbService = mongoDbService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AddUserRequestDTO user)
        {
            var userDomainModel = _mapper.Map<UserClass>(user);
            await _userService.CreateUserAsync(userDomainModel);
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