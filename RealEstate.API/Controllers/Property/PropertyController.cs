using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Services;
using RealEstate.Infrastructure.Services;
using System.Security.Claims;

namespace RealEstate.API.Controllers.Property
{
    [ApiController]
    [Route("api/properties")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _propertyService.GetAllPropertiesAsync();

            if (properties == null || !properties.Any())
            {
                return Ok(new
                {
                    message = "No properties found"
                });
            }

            return Ok(new
            {
                message = "Properties retrieved successfully",
                data = properties
            });
        }
    }
}
