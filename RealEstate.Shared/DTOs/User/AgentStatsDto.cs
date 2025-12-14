using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Shared.DTOs.User
{
    public class AgentStatsDto
    {
        public int TotalPropertyListed { get; set; }
        public int TotalSales { get; set; }
        public decimal AvgRating { get; set; }
        public int YearsExperience { get; set; }
    }
}
