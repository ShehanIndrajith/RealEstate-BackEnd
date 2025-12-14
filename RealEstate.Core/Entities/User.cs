using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureURL { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? WhatsAppNumber { get; set; }

        // Navigation
        public Agent? Agent { get; set; }
        public Builder? Builder { get; set; }
        public ICollection<UserCertification>? UserCertifications { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<AgentReview>? AgentReviews { get; set; }
        public ICollection<BuilderReview>? BuilderReviews { get; set; }
    }
}
