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
    public class BuilderStats
    {
        [Key]
        public int StatsID { get; set; }
        public int BuilderID { get; set; }

        public int TotalProjects { get; set; }
        public int Completed { get; set; }
        public int Ongoing { get; set; }
        public decimal? AvgRating { get; set; }
        public int TotalSales { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Builder Builder { get; set; }
    }
}
