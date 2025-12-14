using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RealEstateDbContext _context;

        public UserRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserID == userId);
        }

        public async Task<User> GetUserWithDetailsAsync(int userId)
        {
            return await _context.Users
         .Include(u => u.Agent)                       // Will be null if Agent doesn't exist
             .ThenInclude(a => a.AgentExpertise)      // Safe: only if Agent exists
         .Include(u => u.Agent)
             .ThenInclude(a => a.AgentStats)          // Safe: only if Agent exists
         .Include(u => u.Agent)
             .ThenInclude(a => a.Properties)          // Safe: only if Agent exists
         .FirstOrDefaultAsync(u => u.UserID == userId);
        }
    }
}
