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
    public class UserCertification
    {
        [Key]
        public int UserCertificationID { get; set; }
        public int UserID { get; set; }
        public int CertificationID { get; set; }
        public string IssuedBy { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public Certification Certification { get; set; }
    }
}
