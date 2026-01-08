using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Shared.DTOs.Property;


namespace RealEstate.Core.Services
{
    public interface IPropertyService
    {
        Task<List<PropertyListItemDto>> GetAllPropertiesAsync();
        Task<List<PropertyListItemDto>> GetPropertiesByListingTypeAsync(string listingType);
        Task<List<PropertyListItemDto>> GetPropertiesByPropertyTypeAsync(string propertyType);
        Task<List<PropertyListItemDto>> GetPropertiesByCityAsync(string city);
        Task<List<PropertyListItemDto>> GetAllActivePropertiesbyAgentID(int agentId);
        Task<PropertyListItemDto?> GetPropertyByIdAsync(int propertyId);
        Task<bool> DeactivatePropertyAsync(int propertyId);
        Task<List<string>> GetTopCitiesAsync(int count);
    }
}
