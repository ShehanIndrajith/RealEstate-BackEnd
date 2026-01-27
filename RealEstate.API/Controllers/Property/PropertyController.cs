using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Entities;
using RealEstate.Core.Services;
using RealEstate.Infrastructure.Services;
using RealEstate.Shared.DTOs.Property;
using System;
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

        [HttpGet("listing-type/{listingType}")]
        public async Task<IActionResult> GetPropertiesByListingType(string listingType)
        {
            if (string.IsNullOrWhiteSpace(listingType))
            {
                return BadRequest(new { message = "ListingType is required" });
            }

            var properties = await _propertyService.GetPropertiesByListingTypeAsync(listingType);

            if (properties == null || !properties.Any())
            {
                return Ok(new
                {
                    message = "No properties found",
                    data = properties
                });
            }

            return Ok(new
            {
                message = "Properties retrieved successfully",
                data = properties
            });
        }

        [HttpGet("property-type/{propertyType}")]
        public async Task<IActionResult> GetPropertiesByPropertyType(string propertyType)
        {
            if (string.IsNullOrWhiteSpace(propertyType))
            {
                return BadRequest(new { message = "PropertyType is required" });
            }

            var properties = await _propertyService.GetPropertiesByPropertyTypeAsync(propertyType);

            if (properties == null || !properties.Any())
            {
                return Ok(new
                {
                    message = "No properties found",
                    data = properties
                });
            }

            return Ok(new
            {
                message = "Properties retrieved successfully",
                data = properties
            });
        }

        [HttpGet("city/{city}")]
        public async Task<IActionResult> GetPropertiesByCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest(new { message = "City is required" });
            }

            var properties = await _propertyService.GetPropertiesByCityAsync(city);

            if (properties == null || !properties.Any())
            {
                return Ok(new
                {
                    message = "No properties found",
                    data = properties
                });
            }

            return Ok(new
            {
                message = "Properties retrieved successfully",
                data = properties
            });
        }

        [HttpGet("featured")]
        public async Task<ActionResult<List<PropertyCardDto>>> GetFeaturedProperties()
        {
            var result = await _propertyService.GetAllFeaturedPropertiesAsync();
            return Ok(result);
        }

        [HttpGet("agent/{agentId:int}")]
        public async Task<IActionResult> GetActivePropertiesByAgentId(int agentId)
        {
            if (agentId <= 0)
            {
                return BadRequest(new { message = "AgentID must be greater than zero" });
            }

            var properties = await _propertyService.GetAllActivePropertiesbyAgentID(agentId);

            if (properties == null || !properties.Any())
            {
                return Ok(new
                {
                    message = "No properties found",
                    data = properties
                });
            }

            return Ok(new
            {
                message = "Properties retrieved successfully",
                data = properties
            });
        }

        [HttpGet("{propertyId:int}")]
        public async Task<IActionResult> GetPropertyById(int propertyId)
        {
            if (propertyId <= 0)
            {
                return BadRequest(new { message = "PropertyID must be greater than zero" });
            }

            var property = await _propertyService.GetPropertyDetailsByIdAsync(propertyId);
            if (property == null)
            {
                return NotFound(new { message = "Property not found" });
            }

            return Ok(new
            {
                message = "Property retrieved successfully",
                data = property
            });
        }

        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> DeactivateProperty(int propertyId)
        {
            if (propertyId <= 0)
            {
                return BadRequest(new { message = "PropertyID must be greater than zero" });
            }

            var updated = await _propertyService.DeactivatePropertyAsync(propertyId);
            if (!updated)
            {
                return NotFound(new { message = "Property not found" });
            }

            return Ok(new { message = "Property deactivated successfully" });
        }

        [HttpGet("top-cities")]
        public async Task<IActionResult> GetTopCities()
        {
            var cities = await _propertyService.GetTopCitiesAsync(5);

            if (cities == null || !cities.Any())
            {
                return Ok(new
                {
                    message = "No cities found",
                    data = cities
                });
            }

            return Ok(new
            {
                message = "Top cities retrieved successfully",
                data = cities
            });
        }

        [HttpGet("{propertyId:int}/media")]
        [Produces("application/json")]
        public async Task<ActionResult<List<PropertyMedia>>> GetPropertyMedia(int propertyId)
        {
            var result = await _propertyService.GetAllMediaByPropertyIdAsync(propertyId);

            // Optional: return 404 if there are no media items for that property
            if (result == null || result.Count == 0)
                return NotFound(new { message = "No media found for the given PropertyID." });

            return Ok(result);
        }

        [HttpGet("{propertyId:int}/description-features")]
        [Produces("application/json")]
        public async Task<ActionResult<PropertyDescriptionDto>> GetDescriptionAndFeatures(int propertyId)
        {
            var result = await _propertyService.GetPropertyDescriptionWithFeaturesAsync(propertyId);

            if (result == null)
                return NotFound(new { message = "Property not found for the given PropertyID." });

            return Ok(result);
        }

        [HttpGet("{propertyId:int}/amenity-ids")]
        [Produces("application/json")]
        public async Task<ActionResult<List<int>>> GetAmenityIds(int propertyId)
        {
            var ids = await _propertyService.GetAmenityIdsByPropertyIdAsync(propertyId);

            // Always return a list (empty if none)
            return Ok(ids ?? new List<int>());
        }
    }
}
