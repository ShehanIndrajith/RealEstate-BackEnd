using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities
{
    public class AgentStats
    {
        [Key]
        public int StatsID { get; set; }
        public int AgentID { get; set; }

        public int TotalPropertiesListed { get; set; }
        public int TotalSales { get; set; }
        public decimal AvgRating { get; set; }
        public int YearsExperience { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Agent? Agent { get; set; }
    }
}
