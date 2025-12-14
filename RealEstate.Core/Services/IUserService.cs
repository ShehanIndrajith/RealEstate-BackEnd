using RealEstate.Core.Entities;
using RealEstate.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Services
{
    public interface IUserService
    {
        Task RegisterUserAsync(CreateUserRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<CurrentUserResponse> GetCurrentUserAsync(int userId);
        Task<User> GetUserWithDetailsAsync(int userId);
    }
}
