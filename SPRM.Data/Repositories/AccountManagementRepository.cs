using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class AccountManagementRepository : IAccountManagementRepository
    {
        private readonly SPRMDbContext _context;

        public AccountManagementRepository(SPRMDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AssignRoleAsync(Guid userId, string role)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Role = role;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateSystemSettingsAsync(SystemSetting settings)
        {
            _context.SystemSettings.Update(settings);
            await _context.SaveChangesAsync();
        }

        public async Task<SystemSetting> GetSettingsAsync()
        {
            return await _context.SystemSettings.FirstOrDefaultAsync() 
                   ?? new SystemSetting();
        }
    }
}
