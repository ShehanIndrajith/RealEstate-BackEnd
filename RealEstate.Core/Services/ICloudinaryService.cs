using Microsoft.AspNetCore.Http;
using RealEstate.Core.Entities;
//using RealEstate.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file, string folderPath);
        Task<bool> DeleteImageAsync(string publicId);
    }
}
