using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Entities;

namespace RealEstate.Core.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllPropertiesAsync();
        Task<List<Property>> GetAllActivePropertiesAsync();
        Task<List<Property>> SearchPropertiesAsync(string? propertyType, string? listingType, string? city, int? agentId);
        Task<Property?> GetPropertyDetailsByIdAsync(int propertyId);
        Task<List<string>> GetTopCitiesAsync(int count);
        Task UpdateAsync(Property property);
    }
}