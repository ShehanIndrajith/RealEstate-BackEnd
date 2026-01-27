using RealEstate.Core.Entities;
using RealEstate.Shared.DTOs.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllPropertiesAsync();
        Task<List<PropertyCardDto>> GetAllActivePropertiesAsync();
        Task<List<PropertyCardDto>> GetAllFeaturedPropertiesAsync();
        Task<List<Property>> SearchPropertiesAsync(string? propertyType, string? listingType, string? city, int? agentId);
        //Task<Property?> GetPropertyDetailsByIdAsync(int propertyId);
        Task<List<string>> GetTopCitiesAsync(int count);
        Task UpdateAsync(Property property);
        Task<List<PropertyMedia>> GetAllMediaByPropertyIdAsync(int propertyId);
        Task<PropertyDetailsDto?> GetPropertyDetailsByIdAsync(int propertyId);
        Task<PropertyDescriptionDto?> GetPropertyDescriptionWithFeaturesAsync(int propertyId);
        Task<List<int>> GetAmenityIdsByPropertyIdAsync(int propertyId);

    }
}