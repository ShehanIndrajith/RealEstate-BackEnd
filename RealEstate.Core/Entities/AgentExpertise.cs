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
    public class AgentExpertise
    {
        [Key]
        public int ExpertiseID { get; set; }
        public int AgentID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Agent Agent { get; set; }
    }
}
