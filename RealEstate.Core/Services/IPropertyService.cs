using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Entities;
using RealEstate.Shared.DTOs.User;


namespace RealEstate.Core.Services
{
    public interface IPropertyService
    {
        Task<List<Property>> GetAllPropertiesAsync();
    }
}
