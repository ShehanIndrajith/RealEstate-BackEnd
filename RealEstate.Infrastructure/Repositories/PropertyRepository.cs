using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
using RealEstate.Shared.DTOs.Property;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly RealEstateDbContext _context;

        public PropertyRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await _context.Properties
        .Include(p => p.Agent)
        .Include(p => p.PropertyMedia)
        .Include(p => p.PropertyFeatures)
        .Include(p => p.PropertyAmenities)
        .Include(p => p.PropertyNearby)
        .Include(p => p.PropertyInquiries)
        .ToListAsync();
        }


        public async Task<List<PropertyCardDto>> GetAllActivePropertiesAsync()
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.IsActive && p.ExpiredAt == null)
                .Select(p => new PropertyCardDto
                {
                    Id = p.PropertyID,
                    Image = p.PropertyMedia
                        .Where(m => m.IsPrimary)
                        .OrderBy(m => m.DisplayOrder)
                        .Select(m => m.MediaURL)
                        .FirstOrDefault(),

                    Price = p.Price,
                    Title = p.Title,
                    Location = (p.City ?? "")
                               + (p.State != null && p.State != "" ? ", " + p.State : ""),
                    Bedrooms = p.Bedrooms,
                    Bathrooms = p.Bathrooms,
                    Area = p.AreaSqFt,
                    Type = p.PropertyType
                })
                .ToListAsync();
        }

        public async Task<List<PropertyCardDto>> GetAllFeaturedPropertiesAsync()
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.IsFeatured && p.IsActive && p.ExpiredAt == null)
                .Select(p => new PropertyCardDto
                {
                    Id = p.PropertyID,
                    Image = p.PropertyMedia
                        .Where(m => m.IsPrimary)
                        .OrderBy(m => m.DisplayOrder)
                        .Select(m => m.MediaURL)
                        .FirstOrDefault(),

                    Price = p.Price,
                    Title = p.Title,
                    Location = (p.City ?? "")
                               + (p.State != null && p.State != "" ? ", " + p.State : ""),
                    Bedrooms = p.Bedrooms,
                    Bathrooms = p.Bathrooms,
                    Area = p.AreaSqFt,
                    Type = p.PropertyType
                })
                .ToListAsync();
        }



        public async Task<List<Property>> SearchPropertiesAsync(string? propertyType,string? listingType, string? city, int? agentId)
        {
            IQueryable<Property> q = _context.Properties.AsNoTracking();


            if (!string.IsNullOrWhiteSpace(propertyType))
                q = q.Where(p => p.PropertyType == propertyType && p.IsActive && p.ExpiredAt == null);

            if (!string.IsNullOrWhiteSpace(listingType))
                q = q.Where(p => p.ListingType == listingType && p.IsActive && p.ExpiredAt == null);

            if (!string.IsNullOrWhiteSpace(city))
                q = q.Where(p => p.City == city && p.IsActive && p.ExpiredAt == null);

            if (agentId > 0)
                q = q.Where(p => p.AgentID == agentId && p.IsActive && p.ExpiredAt == null);

            q = q.Include(p => p.Agent)
                 .Include(p => p.PropertyMedia);

            return await q.ToListAsync();
        }

        //public async Task<Property?> GetPropertyDetailsByIdAsync(int propertyId)
        //{
        //    return await _context.Properties
        //        .AsNoTracking()
        //        .Where(p => p.PropertyID == propertyId)
        //        .Include(p => p.Agent)
        //        .Include(p => p.PropertyMedia)
        //        .Include(p => p.PropertyFeatures)
        //        .Include(p => p.PropertyAmenities)
        //        .Include(p => p.PropertyNearby)
        //        .Include(p => p.PropertyInquiries)
        //        .FirstOrDefaultAsync();
        //}

        public async Task<List<string>> GetTopCitiesAsync(int count)
        {
            if (count <= 0) return new List<string>();

            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.City != null && p.City.Trim() != "")
                .GroupBy(p => p.City.Trim())
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .Take(count)
                .Select(g => g.Key)
                .ToListAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PropertyMedia>> GetAllMediaByPropertyIdAsync(int propertyId)
        {
            return await _context.PropertyMedia
                .AsNoTracking()
                .Where(m => m.PropertyID == propertyId)
                .OrderByDescending(m => m.IsPrimary)   // primary first
                .ThenBy(m => m.DisplayOrder)           // then display order
                .ThenBy(m => m.MediaID)                // stable ordering
                .ToListAsync();
        }

        public async Task<PropertyDetailsDto?> GetPropertyDetailsByIdAsync(int propertyId)
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.PropertyID == propertyId && p.IsActive && p.ExpiredAt == null)
                .Select(p => new PropertyDetailsDto
                {
                    PropertyId = p.PropertyID,

                    Title = p.Title,
                    Price = p.Price,

                    Location = (p.City ?? "")
                               + (!string.IsNullOrWhiteSpace(p.State) ? ", " + p.State : ""),

                    ListingType = p.ListingType,

                    Bedrooms = p.Bedrooms,
                    Bathrooms = p.Bathrooms,
                    Area = p.AreaSqFt,
                    Parking = p.Parking,
                    PropertyType = p.PropertyType,
                    YearBuilt = p.YearBuilt
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PropertyDescriptionDto?> GetPropertyDescriptionWithFeaturesAsync(int propertyId)
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.PropertyID == propertyId)
                .Select(p => new PropertyDescriptionDto
                {
                    PropertyId = p.PropertyID,
                    Description = p.Description,

                    Features = p.PropertyFeatures
                        .OrderBy(f => f.FeatureID)
                        .Select(f => new PropertyFeatureDto
                        {
                            FeatureId = f.FeatureID,
                            FeatureName = f.FeatureName,
                            FeatureDescription = f.FeatureDescription
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<int>> GetAmenityIdsByPropertyIdAsync(int propertyId)
        {
            return await _context.PropertyAmenities
                .AsNoTracking()
                .Where(pa => pa.PropertyID == propertyId)
                .OrderBy(pa => pa.AmenityID)
                .Select(pa => pa.AmenityID)
                .ToListAsync();
        }
    }
}
