using System;
using RealEstate.Shared.DTOs.User;

namespace RealEstate.Shared.DTOs.Property
{
    public class PropertyListItemDto
    {
        public int PropertyID { get; set; }
        public int? AgentID { get; set; }
        public string Title { get; set; }
        public string PropertyType { get; set; }
        public string Status { get; set; }
        public string ListingType { get; set; }

        public decimal? Price { get; set; }
        public int? AreaSqFt { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? Parking { get; set; }

        public string? Description { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }

        public string City { get; set; }
        public string? State { get; set; }
        public string Country { get; set; }
        public string? ZipCode { get; set; }

        public bool IsFeatured { get; set; }
        public decimal? PricePerSqFt { get; set; }
        public int? YearBuilt { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }

        public AgentDto? Agent { get; set; }
    }
}
