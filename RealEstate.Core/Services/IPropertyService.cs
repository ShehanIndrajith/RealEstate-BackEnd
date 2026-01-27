using RealEstate.Core.Entities;
using RealEstate.Shared.DTOs.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealEstate.Core.Services
{
    public interface IPropertyService
    {
        Task<List<PropertyCardDto>> GetAllPropertiesAsync();
        Task<List<PropertyListItemDto>> GetPropertiesByListingTypeAsync(string listingType);
        Task<List<PropertyListItemDto>> GetPropertiesByPropertyTypeAsync(string propertyType);
        Task<List<PropertyListItemDto>> GetPropertiesByCityAsync(string city);
        Task<List<PropertyCardDto>> GetAllFeaturedPropertiesAsync();
        Task<List<PropertyListItemDto>> GetAllActivePropertiesbyAgentID(int agentId);
        Task<PropertyDetailsDto?> GetPropertyDetailsByIdAsync(int propertyId);
        Task<bool> DeactivatePropertyAsync(int propertyId);
        Task<List<string>> GetTopCitiesAsync(int count);
        Task<List<PropertyMedia>> GetAllMediaByPropertyIdAsync(int propertyId);
        Task<PropertyDescriptionDto?> GetPropertyDescriptionWithFeaturesAsync(int propertyId);
        Task<List<int>> GetAmenityIdsByPropertyIdAsync(int propertyId);
    }
}
