using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
using RealEstate.Core.Services;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IBuilderRepository _builderRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly Cloudinary _cloudinary;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(
            IUserRepository userRepository,IAgentRepository agentRepository,
            Cloudinary cloudinary, JwtTokenGenerator jwtTokenGenerator, IBuilderRepository builderRepository)
        {
            _userRepository = userRepository;
            _agentRepository = agentRepository;
            _cloudinary = cloudinary;
            _passwordHasher = new PasswordHasher<User>();
            _jwtTokenGenerator = jwtTokenGenerator;
            _builderRepository = builderRepository;
        }

        public async Task RegisterUserAsync(CreateUserRequest request)
        {
            // Check existing user
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception("Email already exists");

            string? imageUrl = null;

            // Upload image
            if (request.ProfileImage != null)
            {
                using var stream = request.ProfileImage.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(request.ProfileImage.FileName, stream),
                    Folder = "users/profile-pictures"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                imageUrl = uploadResult.SecureUrl.ToString();
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Role = request.Role,
                PhoneNumber = request.PhoneNumber,
                ProfilePictureURL = imageUrl,
                IsVerified = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            await _userRepository.AddAsync(user);

            // 🔥 Role-based creation
            if (request.Role == "Agent")
            {
                var agent = new Agent
                {
                    User = user, // EF will set UserID automatically
                    IsVerified = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _agentRepository.AddAsync(agent);
            }
            else if (request.Role == "Builder")
            {
                var builder = new Builder
                {
                    User = user,
                    IsVerified = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _builderRepository.AddAsync(builder);
            }

            // ✅ One transaction, one SaveChanges
            await _userRepository.SaveChangesAsync();
        }


        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || !user.IsActive)
                throw new Exception("Invalid credentials");

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid credentials");

            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.SaveChangesAsync();

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                Role = user.Role,
                FullName = user.FullName
            };
        }

        public async Task<CurrentUserResponse> GetCurrentUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null || !user.IsActive)
                throw new Exception("User not found");

            return new CurrentUserResponse
            {
                UserID = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
                ProfilePictureURL = user.ProfilePictureURL,
                IsVerified = user.IsVerified
            };
        }

        public async Task<User> GetUserWithDetailsAsync(int userId)
        {
            return await _userRepository.GetUserWithDetailsAsync(userId);
        }
    }
}
