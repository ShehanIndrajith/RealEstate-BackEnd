using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Core.Entities
{
    public class PropertyAmenities
    {
        [Key]
        public int PropertyAmenityID { get; set; }
        public int PropertyID { get; set; }
        public int AmenityID { get; set; }

        public Property Property { get; set; }
        public Amenity Amenity { get; set; }
    }
}
