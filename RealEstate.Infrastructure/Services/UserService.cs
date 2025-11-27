using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
using RealEstate.Core.Services;
using RealEstate.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Users> CreateUserAsync(UserCreateDto dto)
        {
            // Check email duplicate
            if (await _userRepository.EmailExistsAsync(dto.Email))
                throw new Exception("Email already exists.");

            string hashedPassword = HashPassword(dto.Password);

            var user = new Users
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Role = dto.Role,
                PhoneNumber = dto.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsVerified = false
            };

            return await _userRepository.CreateAsync(user);
        }

        private string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            byte[] hash = KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        public async Task<List<Users>> GetAllUsersByRolesAsync(string role)
        {
            return await _userRepository.GetAllUsersByRoleAsync(role);
        }

        public async Task<List<Users>> GetAllAgentsAsync()
        {
            return await _userRepository.GetAllAgentsAsync();
        }

        public async Task<List<Users>> GetAllBuildersAsync()
        {
            return await _userRepository.GetAllBuildersAsync();
        }

        public async Task<Users> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }
    }
}
