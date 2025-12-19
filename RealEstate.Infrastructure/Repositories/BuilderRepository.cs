using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;

namespace RealEstate.Infrastructure.Repositories
{
    public class BuilderRepository : IBuilderRepository
    {
        private readonly RealEstateDbContext _context;

        public BuilderRepository(RealEstateDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Builder builder)
        {
            await _context.Builders.AddAsync(builder);
        }
    }
}
