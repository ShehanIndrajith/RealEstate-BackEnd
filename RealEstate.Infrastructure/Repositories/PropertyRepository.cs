using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly RealEstateDbContext _context;
        public PropertyRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<List<Properties>> GetAllAsync()
        {
            return await _context.Properties
                .Where(p => p.ExpiredAt == null)
                .ToListAsync();
        }

        public async Task<List<Properties>> GetPropertiesByUserIdAsync(int userId)
        {
            return await _context.Properties
                .Where(p => p.UserID == userId && p.ExpiredAt == null)
                .ToListAsync();
        }

        public async Task<Properties> GetByIdAsync(int id)
        {
            return await _context.Properties.FindAsync(id);
        }

        public async Task<List<Properties>> GetPropertiesByStatusAsync(string status)
        {
            return await _context.Properties
                .Where(p => p.Status == status && p.ExpiredAt == null)
                .ToListAsync();
        }

        public async Task<List<Properties>> GetPropertiesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Properties
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.ExpiredAt == null)
                .ToListAsync();
        }

        public async Task<List<Properties>> GetPropertiesByCityAsync(string city)
        {
            return await _context.Properties
                .Where(p => p.City.Contains(city) && p.ExpiredAt == null)
                .ToListAsync();
        }

        public async Task<List<Properties>> GetPropertiesByTypeAsync(string type)
        {
            return await _context.Properties
                .Where(p => p.PropertyType == type && p.ExpiredAt == null)
                .ToListAsync();
        }

        public async Task<bool> UpdateExpiredAtAsync(int propertyId)
        {
            var property = await _context.Properties
                .FirstOrDefaultAsync(p => p.PropertyID == propertyId);

            if (property == null)
                return false;

            property.ExpiredAt = DateTime.UtcNow; // or DateTime.Now based on your preference
            property.UpdatedAt = DateTime.UtcNow; // optional but recommended

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
