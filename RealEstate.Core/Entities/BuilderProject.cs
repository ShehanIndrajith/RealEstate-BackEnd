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
    public class BuilderProject
    {
        [Key]
        public int ProjectID { get; set; }
        public int BuilderID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public decimal? PriceRangeStart { get; set; }
        public decimal? PriceRangeEnd { get; set; }
        public string PriceNote { get; set; }

        public string ProjectType { get; set; }
        public string Status { get; set; }

        public int? UnitsTotal { get; set; }
        public int? UnitsAvailable { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Builder Builder { get; set; }
        public ICollection<ProjectMedia> ProjectMedia { get; set; }
    }
}
