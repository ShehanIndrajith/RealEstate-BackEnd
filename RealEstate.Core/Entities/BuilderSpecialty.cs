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
    public class BuilderSpecialty
    {
        [Key]
        public int BuilderSpecialtyID { get; set; }
        public int BuilderID { get; set; }
        public int SpecialtyID { get; set; }
        public DateTime CreatedAt { get; set; }

        public Builder Builder { get; set; }
        public Specialty Specialty { get; set; }
    }
}
