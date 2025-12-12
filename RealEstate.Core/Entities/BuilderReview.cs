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
    public class BuilderReview
    {
        [Key]
        public int ReviewID { get; set; }
        public int BuilderID { get; set; }
        public int? ReviewerID { get; set; }

        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        public string ReviewerImageURL { get; set; }
        public byte Rating { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int HelpfulCount { get; set; }
        public DateTime CreatedAt { get; set; }

        public Builder Builder { get; set; }
        public User Reviewer { get; set; }
    }
}
