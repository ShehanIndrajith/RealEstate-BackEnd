using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Core.Entities
{
    public class Amenity
    {
        [Key]
        public int AmenityID { get; set; }
        public string AmenityName { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<PropertyAmenities> PropertyAmenities { get; set; }
    }
}
