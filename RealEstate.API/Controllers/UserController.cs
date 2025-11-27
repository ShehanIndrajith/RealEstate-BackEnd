using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Interfaces;
using RealEstate.Core.Services;
using RealEstate.Shared.DTOs.User;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            try
            {
                var result = await _userService.CreateUserAsync(dto);
                return Ok(new { Message = "User created successfully.", UserID = result.UserID });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("agents/{role}")]
        public async Task<IActionResult> GetAllAgents(string role)
        {
            var agents = await _userService.GetAllUsersByRolesAsync(role);
            return Ok(agents);
        }

        [HttpGet("agents")]
        public async Task<IActionResult> GetAllAgents()
        {
            var agents = await _userService.GetAllAgentsAsync();
            return Ok(agents);
        }

        [HttpGet("builders")]
        public async Task<IActionResult> GetAllBuilders()
        {
            var builders = await _userService.GetAllBuildersAsync();
            return Ok(builders);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("user/{UserID}")]
        public async Task<IActionResult> GetUserDetails(int UserID)
        {
            var user = await _userService.GetUserByIdAsync(UserID);

            if (user == null)
                return NotFound(new { Message = "User not found." });

            return Ok(user);
        }

    }


}
