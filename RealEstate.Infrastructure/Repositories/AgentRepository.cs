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
    public class AgentRepository : IAgentRepository
    {
        private readonly RealEstateDbContext _context;

        public AgentRepository(RealEstateDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Agent agent)
        {
            await _context.Agents.AddAsync(agent);
        }
    }
}
