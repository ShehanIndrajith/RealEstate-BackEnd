using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Core.Entities
{
    public class PropertyInquiry
    {
        [Key]
        public int InquiryID { get; set; }
        public int PropertyID { get; set; }

        public string InquirerName { get; set; }
        public string InquirerEmail { get; set; }
        public string InquirerPhone { get; set; }
        public string Message { get; set; }

        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ResponseAt { get; set; }

        public Property Property { get; set; }
    }
}
