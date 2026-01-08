using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
using RealEstate.Core.Services;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Shared.DTOs.User;

namespace RealEstate.Infrastructure.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly ICloudinaryService _cloudinaryService;
        public PropertyService(
            IPropertyRepository propertyRepository,
            IUserRepository userRepository,
            IAgentRepository agentRepository,
            ICloudinaryService cloudinaryService)
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _agentRepository = agentRepository;
            _cloudinaryService = cloudinaryService;
        }

        public Task<List<Property>> GetAllPropertiesAsync()
        {
            return _propertyRepository.GetAllPropertiesAsync();
        }
    }
}
