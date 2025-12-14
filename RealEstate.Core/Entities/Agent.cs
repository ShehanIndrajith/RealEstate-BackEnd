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
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }
        public int UserID { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public int? ExperienceYears { get; set; }
        public string? NationalRanking { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public User User { get; set; }
        public ICollection<AgentExpertise>? AgentExpertise { get; set; }
        public ICollection<AgentReview>? AgentReviews { get; set; }
        public AgentStats? AgentStats { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }

}
