using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(SPRMDbContext context) : base(context) {}

    public async Task<IEnumerable<UserRole>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserRoles
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserRole>> GetByRoleAsync(string role)
    {
        return await _context.UserRoles
            .Where(r => r.Role == role)
            .ToListAsync();
    }
}

}