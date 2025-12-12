using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Core.Entities
{
    public class PropertyMedia
    {
        [Key]
        public int MediaID { get; set; }
        public int PropertyID { get; set; }

        public string MediaType { get; set; }
        public string MediaURL { get; set; }

        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }

        public Property Property { get; set; }
    }

}
