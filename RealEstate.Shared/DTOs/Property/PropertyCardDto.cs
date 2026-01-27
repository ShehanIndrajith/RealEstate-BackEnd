using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Shared.DTOs.Property
{
    public class PropertyCardDto
    {
        public int Id { get; set; }
        public string? Image { get; set; }          // MediaURL (primary)
        public decimal? Price { get; set; }
        public string Title { get; set; } = "";
        public string Location { get; set; } = "";  // e.g., "City, State"
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? Area { get; set; }            // AreaSqFt
        public string Type { get; set; } = "";      // PropertyType
    }
}
