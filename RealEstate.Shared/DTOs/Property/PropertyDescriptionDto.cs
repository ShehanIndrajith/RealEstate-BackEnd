using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Shared.DTOs.Property
{
    public class PropertyDescriptionDto
    {
        public int PropertyId { get; set; }
        public string Description { get; set; } = "";
        public List<PropertyFeatureDto> Features { get; set; } = new();
    }

}
