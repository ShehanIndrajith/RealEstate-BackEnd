using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Core.Entities
{
    public class PropertyFeatures
    {
        [Key]
        public int FeatureID { get; set; }
        public int PropertyID { get; set; }

        public string FeatureName { get; set; }
        public string FeatureDescription { get; set; }
        public DateTime CreatedAt { get; set; }

        public Property Property { get; set; }
    }
}
