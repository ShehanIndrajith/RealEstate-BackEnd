using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
using RealEstate.Core.Services;
using Microsoft.AspNetCore.Http;
using RealEstate.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace RealEstate.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folderPath)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = folderPath // e.g.: RealEstateAssets/Users/123
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
                throw new Exception(result.Error.Message);

            return result.SecureUrl.ToString(); // Save this to DB
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }
    }

}
