using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Core.Entities
{
    public class PropertyNearby
    {
        [Key]
        public int NearbyID { get; set; }
        public int PropertyID { get; set; }

        public string PlaceName { get; set; }
        public string PlaceType { get; set; }
        public string Distance { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime CreatedAt { get; set; }

        public Property Property { get; set; }
    }
}
