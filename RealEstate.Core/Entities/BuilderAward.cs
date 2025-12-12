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
    public class BuilderAward
    {
        [Key]
        public int AwardID { get; set; }
        public int BuilderID { get; set; }

        public string Title { get; set; }
        public int? Year { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public Builder Builder { get; set; }
    }
}
