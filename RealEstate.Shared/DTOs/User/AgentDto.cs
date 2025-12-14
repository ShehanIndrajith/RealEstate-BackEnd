using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Shared.DTOs.User
{
    public class AgentDto
    {
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public int? ExperienceYears { get; set; }
        public AgentStatsDto? Stats { get; set; }
        public ICollection<AgentExpertiseDto>? Expertise { get; set; }
    }
}
