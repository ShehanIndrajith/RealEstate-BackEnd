
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealEstate.Core.Entities
{
    public class Property
    {
        [Key]
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

        public Agent Agent { get; set; }

        public ICollection<PropertyMedia> PropertyMedia { get; set; }
        public ICollection<PropertyFeatures> PropertyFeatures { get; set; }
        public ICollection<PropertyAmenities> PropertyAmenities { get; set; }
        public ICollection<PropertyNearby> PropertyNearby { get; set; }
        public ICollection<PropertyInquiry> PropertyInquiries { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
