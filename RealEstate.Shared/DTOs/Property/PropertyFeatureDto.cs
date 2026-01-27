using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Shared.DTOs.Property
{
    public class PropertyFeatureDto
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; } = "";
        public string FeatureDescription { get; set; } = "";
    }
}
