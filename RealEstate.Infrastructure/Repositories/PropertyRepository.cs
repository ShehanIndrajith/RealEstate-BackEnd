using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
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


        public async Task<List<Property>> GetAllActivePropertiesAsync()
        {
            return await _context.Properties
              .Where(p => p.IsActive && p.ExpiredAt == null)
        .Include(p => p.Agent)
        .Include(p => p.PropertyMedia)
        .Include(p => p.PropertyFeatures)
        .Include(p => p.PropertyAmenities)
        .Include(p => p.PropertyNearby)
        .Include(p => p.PropertyInquiries)
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

        public async Task<Property?> GetPropertyDetailsByIdAsync(int propertyId)
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.PropertyID == propertyId)
                .Include(p => p.Agent)
                .Include(p => p.PropertyMedia)
                .Include(p => p.PropertyFeatures)
                .Include(p => p.PropertyAmenities)
                .Include(p => p.PropertyNearby)
                .Include(p => p.PropertyInquiries)
                .FirstOrDefaultAsync();
        }

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



    }
}
