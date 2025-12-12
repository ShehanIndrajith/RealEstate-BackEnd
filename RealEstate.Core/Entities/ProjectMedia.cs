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
    public class ProjectMedia
    {
        [Key]
        public int MediaID { get; set; }
        public int ProjectID { get; set; }

        public string URL { get; set; }
        public string MediaType { get; set; }
        public string Caption { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsHero { get; set; }
        public DateTime UploadedAt { get; set; }

        public BuilderProject Project { get; set; }
    }
}
