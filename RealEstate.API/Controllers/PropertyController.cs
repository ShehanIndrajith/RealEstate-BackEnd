using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Interfaces;
using RealEstate.Core.Services;
using RealEstate.Infrastructure.Services;
using RealEstate.Shared.DTOs.User;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : Controller
    {
        private readonly IPropertyService propertyService;
        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await propertyService.getAllProperties();
            return Ok(properties);
        }

        [HttpGet("agent/properties/{userId}")]
        public async Task<IActionResult> GetPropertiesByUserId(int userId)
        {
            var properties = await propertyService.GetPropertiesByUserIdAsync(userId);
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            var property = await propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            return Ok(property);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetPropertiesByStatus(string status)
        {
            var properties = await propertyService.GetPropertiesByStatusAsync(status);
            return Ok(properties);

        }

        [HttpGet("price-range")]
        public async Task<IActionResult> GetPropertiesByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var properties = await propertyService.GetPropertiesByPriceRangeAsync(minPrice, maxPrice);
            return Ok(properties);

        }

        [HttpGet("city/{city}")]
        public async Task<IActionResult> GetPropertiesByCity(string city)
        {
            var properties = await propertyService.GetPropertiesByCityAsync(city);
            return Ok(properties);

        }
        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetPropertiesByType(string type)
        {
            var properties = await propertyService.GetPropertiesByTypeAsync(type);
            return Ok(properties);
        }

        [HttpPut("expire/{id}")]
        public async Task<IActionResult> ExpireProperty(int id)
        {
            var result = await propertyService.MarkPropertyExpiredAsync(id);

            if (!result)
                return NotFound("Property not found.");

            return Ok("Property expired successfully.");
        }
    }
}
