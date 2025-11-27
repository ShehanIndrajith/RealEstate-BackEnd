using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Shared.DTOs.User;

namespace RealEstate.Core.Services
{
    public interface IUserService
    {
        Task<List<Users>> GetAllUsersAsync();
        Task<Users> CreateUserAsync(UserCreateDto dto);
        Task<List<Users>> GetAllUsersByRolesAsync(string role);
        Task<List<Users>> GetAllAgentsAsync();
        Task<List<Users>> GetAllBuildersAsync();
        Task<Users> GetUserByIdAsync(int userId);
    }
}
