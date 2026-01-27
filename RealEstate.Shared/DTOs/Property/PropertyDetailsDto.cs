using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Shared.DTOs.Property
{
    public class PropertyDetailsDto
    {
        public int PropertyId { get; set; }

        // PropertyHeader
        public string Title { get; set; } = "";
        public decimal? Price { get; set; }
        public string Location { get; set; } = "";     // City + State (or full address if you prefer)
        public string ListingType { get; set; } = "";  // e.g., "Rent" / "Sale"

        // QuickFacts
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public double? Area { get; set; }               // AreaSqFt
        public int? Parking { get; set; }
        public string PropertyType { get; set; } = ""; // e.g., "House" / "Apartment"
        public int? YearBuilt { get; set; }
    }

}
