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
    public class AgentReview
    {
        [Key]
        public int ReviewID { get; set; }
        public int AgentID { get; set; }
        public int? ReviewerID { get; set; }

        public string ReviewerName { get; set; }
        public string ReviewerImage { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; }
        public int HelpfulCount { get; set; }
        public DateTime ReviewDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Agent Agent { get; set; }
        public User Reviewer { get; set; }
    }
}
