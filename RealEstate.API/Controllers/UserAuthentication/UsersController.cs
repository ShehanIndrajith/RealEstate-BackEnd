using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Services;
using RealEstate.Shared.DTOs.User;
using System.Security.Claims;

namespace RealEstate.API.Controllers.UserAuthentication
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] CreateUserRequest request)
        {
            await _userService.RegisterUserAsync(request);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _userService.LoginAsync(request);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                           ?? User.FindFirst("sub");

            int userId = int.Parse(userIdClaim.Value);

            var user = await _userService.GetCurrentUserAsync(userId);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("me/details")]
        public async Task<IActionResult> GetMyDetails()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var user = await _userService.GetUserWithDetailsAsync(userId);

            if (user == null)
                return NotFound(new { message = "User not found" });

            var userDto = new UserDetailsDto
            {
                UserID = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                IsVerified = user.IsVerified,
                ProfilePictureURL = user.ProfilePictureURL,
                Agent = user.Agent != null ? new AgentDto
                {
                    Bio = user.Agent.Bio,
                    Location = user.Agent.Location,
                    ExperienceYears = user.Agent.ExperienceYears,
                    Stats = user.Agent.AgentStats != null ? new AgentStatsDto
                    {
                        TotalPropertyListed = user.Agent.AgentStats.TotalPropertiesListed,
                        TotalSales = user.Agent.AgentStats.TotalSales,
                        AvgRating = user.Agent.AgentStats.AvgRating,
                        YearsExperience = user.Agent.AgentStats.YearsExperience
                    } : null,
                    Expertise = user.Agent.AgentExpertise?.Select(e => new AgentExpertiseDto
                    {
                        Name = e.Name
                    }).ToList()
                } : null
            };

            return Ok(userDto);
        }



    }
}
