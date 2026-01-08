using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Shared.DTOs.User
{
    public class UpdateUserProfileRequest
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WhatsAppNumber { get; set; }
        public string? ProfilePictureURL { get; set; }

        // Agent fields
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public int? ExperienceYears { get; set; }
        public string? NationalRanking { get; set; }

        // Expertise list
        public List<string>? Expertises { get; set; }
    }
}
