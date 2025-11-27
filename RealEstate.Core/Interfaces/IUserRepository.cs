using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Entities;

namespace RealEstate.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllAsync();
        Task<List<Users>> GetAllUsersByRoleAsync(string role);
        Task<List<Users>> GetAllAgentsAsync();
        Task<List<Users>> GetAllBuildersAsync();
        Task<Users> CreateAsync(Users user);
        Task<bool> EmailExistsAsync(string email);
        Task<Users> GetUserByIdAsync(int userId);
    }
}
