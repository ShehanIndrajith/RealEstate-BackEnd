using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
using RealEstate.Core.Services;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Shared.DTOs.Property;
using RealEstate.Shared.DTOs.User;

namespace RealEstate.Infrastructure.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly ICloudinaryService _cloudinaryService;
        public PropertyService(
            IPropertyRepository propertyRepository,
            IUserRepository userRepository,
            IAgentRepository agentRepository,
            ICloudinaryService cloudinaryService)
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _agentRepository = agentRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<List<PropertyListItemDto>> GetAllPropertiesAsync()
        {
            var properties = await _propertyRepository.GetAllActivePropertiesAsync();

            return MapProperties(properties);
        }

        public async Task<List<PropertyListItemDto>> GetPropertiesByListingTypeAsync(string listingType)
        {
            if (string.IsNullOrWhiteSpace(listingType))
            {
                return new List<PropertyListItemDto>();
            }

            var properties = await _propertyRepository.SearchPropertiesAsync(null, listingType, null,0);

            return MapProperties(properties);
        }

        public async Task<List<PropertyListItemDto>> GetPropertiesByPropertyTypeAsync(string propertyType)
        {
            if (string.IsNullOrWhiteSpace(propertyType))
            {
                return new List<PropertyListItemDto>();
            }

            var allowedTypes = new[] { "House", "Apartment", "Land", "Commercial" };
            var isAllowed = allowedTypes.Any(t => string.Equals(t, propertyType, StringComparison.OrdinalIgnoreCase));
            if (!isAllowed)
            {
                return new List<PropertyListItemDto>();
            }

            var properties = await _propertyRepository.SearchPropertiesAsync(propertyType,null,null, 0);

            return MapProperties(properties);
        }

        public async Task<List<PropertyListItemDto>> GetPropertiesByCityAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return new List<PropertyListItemDto>();
            }

            var properties = await _propertyRepository.SearchPropertiesAsync(null, null, city, 0);

            return MapProperties(properties);
        }

        public async Task<List<PropertyListItemDto>> GetAllActivePropertiesbyAgentID(int agentId)
        {
            if (agentId <= 0)
            {
                return new List<PropertyListItemDto>();
            }
            var properties = await _propertyRepository.SearchPropertiesAsync(null,null,null,agentId);
            return MapProperties(properties);
        }

        public async Task<PropertyListItemDto?> GetPropertyByIdAsync(int propertyId)
        {
            if (propertyId <= 0)
            {
                return null;
            }

            var property = await _propertyRepository.GetPropertyDetailsByIdAsync(propertyId);
            if (property == null)
            {
                return null;
            }

            return MapProperties(new[] { property }).FirstOrDefault();
        }

        public async Task<bool> DeactivatePropertyAsync(int propertyId)
        {
            if (propertyId <= 0)
            {
                return false;
            }

            var properties = await _propertyRepository.GetAllPropertiesAsync();
            var property = properties.FirstOrDefault(p => p.PropertyID == propertyId);
            if (property == null)
            {
                return false;
            }

            property.IsActive = false;
            property.ExpiredAt = DateTime.UtcNow;

            await _propertyRepository.UpdateAsync(property);
            return true;
        }

        public async Task<List<string>> GetTopCitiesAsync(int count)
        {
            if (count <= 0) return new List<string>();
            return await _propertyRepository.GetTopCitiesAsync(count);
        }

        private static List<PropertyListItemDto> MapProperties(IEnumerable<Property> properties)
        {
            return properties.Select(p => new PropertyListItemDto
            {
                PropertyID = p.PropertyID,
                AgentID = p.AgentID,
                Title = p.Title,
                PropertyType = p.PropertyType,
                Status = p.Status,
                ListingType = p.ListingType,
                Price = p.Price,
                AreaSqFt = p.AreaSqFt,
                Bedrooms = p.Bedrooms,
                Bathrooms = p.Bathrooms,
                Parking = p.Parking,
                Description = p.Description,
                AddressLine1 = p.AddressLine1,
                AddressLine2 = p.AddressLine2,
                City = p.City,
                State = p.State,
                Country = p.Country,
                ZipCode = p.ZipCode,
                IsFeatured = p.IsFeatured,
                PricePerSqFt = p.PricePerSqFt,
                YearBuilt = p.YearBuilt,
                IsVerified = p.IsVerified,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                ExpiredAt = p.ExpiredAt,
                Agent = p.Agent == null ? null : new AgentDto
                {
                    AgentID = p.Agent.AgentID,
                    Bio = p.Agent.Bio,
                    Location = p.Agent.Location,
                    ExperienceYears = p.Agent.ExperienceYears,
                    IsVerified = p.Agent.IsVerified,
                    Stats = p.Agent.AgentStats == null ? null : new AgentStatsDto
                    {
                        TotalPropertyListed = p.Agent.AgentStats.TotalPropertiesListed,
                        TotalSales = p.Agent.AgentStats.TotalSales,
                        AvgRating = p.Agent.AgentStats.AvgRating,
                        YearsExperience = p.Agent.AgentStats.YearsExperience
                    },
                    Expertise = p.Agent.AgentExpertise == null ? null : p.Agent.AgentExpertise
                        .Select(e => new AgentExpertiseDto { Name = e.Name })
                        .ToList()
                }
            }).ToList();
        }
    }
}
