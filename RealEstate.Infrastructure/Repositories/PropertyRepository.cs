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


    }
}
