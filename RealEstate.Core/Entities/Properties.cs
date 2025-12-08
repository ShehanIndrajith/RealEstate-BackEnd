using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Core.Entities
{
    public class Properties
    {
        [Key]
        public int PropertyID { get; set; } // Primary key, identity

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!; // Not null

        public string? Description { get; set; } // Nullable in DB

        [Required]
        [MaxLength(200)]
        public string AddressLine1 { get; set; } = null!; // Not null

        [MaxLength(200)]
        public string? AddressLine2 { get; set; } // Nullable

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = null!; // Not null

        [MaxLength(100)]
        public string? State { get; set; } // Nullable

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = null!; // Not null

        [MaxLength(20)]
        public string? ZipCode { get; set; } // Nullable

        [Required]
        public decimal Price { get; set; } // Not null

        public int? AreaSqFt { get; set; } // Nullable in DB

        public int? Bedrooms { get; set; } // Nullable

        public int? Bathrooms { get; set; } // Nullable

        public int? Parking { get; set; } // Nullable

        [MaxLength(50)]
        public string? PropertyType { get; set; } // Commercial, Land, Apartment, House

        [MaxLength(50)]
        public string? Status { get; set; } // For Sale, For Rent

        public bool? IsFeatured { get; set; } // Nullable

        public DateTime? CreatedAt { get; set; } // Nullable
        public DateTime? UpdatedAt { get; set; } // Nullable
        public DateTime? ExpiredAt { get; set; } // Nullable

        // Foreign Key to Users table (Agent)
        public int? UserID { get; set; }

        [ForeignKey("UserID")]
        public Users? Users { get; set; } // Navigation property
    }
}
