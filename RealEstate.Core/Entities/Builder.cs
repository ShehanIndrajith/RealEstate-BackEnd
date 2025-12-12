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
    public class Builder
    {
        [Key]
        public int BuilderID { get; set; }
        public int UserID { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int? EstablishedYear { get; set; }
        public string LogoURL { get; set; }
        public string Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Slug { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string WebsiteURL { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public User User { get; set; }
        public BuilderStats BuilderStats { get; set; }
        public ICollection<BuilderSpecialty> BuilderSpecialties { get; set; }
        public ICollection<BuilderReview> BuilderReviews { get; set; }
        public ICollection<BuilderProject> BuilderProjects { get; set; }
        public ICollection<BuilderAward> BuilderAwards { get; set; }
        public ICollection<BuilderContactInquiry> BuilderContactInquiries { get; set; }
    }
}
