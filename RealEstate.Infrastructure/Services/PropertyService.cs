using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
using RealEstate.Core.Services;
using RealEstate.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<List<Properties>> getAllProperties()
        {
            return await _propertyRepository.GetAllAsync();
        }

        public async Task<List<Properties>> GetPropertiesByUserIdAsync(int userId)
        {
            return await _propertyRepository.GetPropertiesByUserIdAsync(userId);
        }

        public async Task<Properties> GetPropertyByIdAsync(int id)
        {
            return await _propertyRepository.GetByIdAsync(id);
        }

        public async Task<List<Properties>> GetPropertiesByTypeAsync(string type)
        {
            return await _propertyRepository.GetPropertiesByTypeAsync(type);
        }

        public async Task<List<Properties>> GetPropertiesByCityAsync(string city)
        {
            return await _propertyRepository.GetPropertiesByCityAsync(city);
        }

        public async Task<List<Properties>> GetPropertiesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _propertyRepository.GetPropertiesByPriceRangeAsync(minPrice, maxPrice);
        }

        public async Task<List<Properties>> GetPropertiesByStatusAsync(string status)
        {
            return await _propertyRepository.GetPropertiesByStatusAsync(status);
        }

        public async Task<bool> MarkPropertyExpiredAsync(int id)
        {
            return await _propertyRepository.UpdateExpiredAtAsync(id);
        }
    }
}
