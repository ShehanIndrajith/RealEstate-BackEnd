using RealEstate.Core.Entities;
using RealEstate.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Services
{
    public interface IPropertyService
    {
        Task<List<Properties>> getAllProperties();
        Task<List<Properties>> GetPropertiesByUserIdAsync(int userId);
        Task<Properties> GetPropertyByIdAsync(int id);
        Task<List<Properties>> GetPropertiesByStatusAsync(string status);
        Task<List<Properties>> GetPropertiesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<List<Properties>> GetPropertiesByCityAsync(string city);
        Task<List<Properties>> GetPropertiesByTypeAsync(string type);
        Task<bool> MarkPropertyExpiredAsync(int id);
    }
}
