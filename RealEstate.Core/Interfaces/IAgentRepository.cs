using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Interfaces
{
    public interface IAgentRepository
    {
        Task AddAsync(Agent agent);
        Task AddAgentExpertisesIfNotExistsAsync(int agentId, List<string> expertises);
    }
}
