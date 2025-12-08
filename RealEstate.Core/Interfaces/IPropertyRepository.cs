using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Entities;

namespace RealEstate.Core.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<Properties>> GetAllAsync();
        Task<List<Properties>> GetPropertiesByUserIdAsync(int userId);
        Task<Properties> GetByIdAsync(int id);
        Task<List<Properties>> GetPropertiesByStatusAsync(string status);
        Task<List<Properties>> GetPropertiesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<List<Properties>> GetPropertiesByCityAsync(string city);
        Task<List<Properties>> GetPropertiesByTypeAsync(string type);
        Task<bool> UpdateExpiredAtAsync(int propertyId);

    }
}
