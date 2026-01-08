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

        public async Task AddAgentExpertisesIfNotExistsAsync(
        int agentId,
        List<string> expertises)
        {
            var existingExpertises = await _context.AgentExpertise
                .Where(e => e.AgentID == agentId)
                .Select(e => e.Name)
                .ToListAsync();

            var newExpertises = expertises
                .Where(e => !existingExpertises.Contains(e))
                .Select(e => new AgentExpertise
                {
                    AgentID = agentId,
                    Name = e,
                    CreatedAt = DateTime.UtcNow
                })
                .ToList();

            if (newExpertises.Any())
            {
                await _context.AgentExpertise.AddRangeAsync(newExpertises);
                await _context.SaveChangesAsync();
            }
        }
    }
}
