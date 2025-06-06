using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class ProjectMembershipRepository : BaseRepository<ProjectMembership>, IProjectMembershipRepository
    {
        public ProjectMembershipRepository(SPRMDbContext context) : base(context) { }

        public new async Task<ProjectMembership?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(pm => pm.Project)
                .Include(pm => pm.User)
                .FirstOrDefaultAsync(pm => pm.Id == id);
        }

        public async Task<IEnumerable<ProjectMembership>> GetByProjectIdAsync(Guid projectId)
        {
            return await _dbSet
                .Include(pm => pm.User)
                .Where(pm => pm.ProjectId == projectId && pm.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProjectMembership>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Include(pm => pm.Project)
                .Where(pm => pm.UserId == userId && pm.IsActive)
                .ToListAsync();
        }

        public async Task<ProjectMembership?> GetByProjectAndUserAsync(Guid projectId, Guid userId)
        {
            return await _dbSet
                .Include(pm => pm.Project)
                .Include(pm => pm.User)
                .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == userId && pm.IsActive);
        }

        public new async Task<IEnumerable<ProjectMembership>> GetAllAsync()
        {
            return await _dbSet
                .Include(pm => pm.Project)
                .Include(pm => pm.User)
                .Where(pm => pm.IsActive)
                .ToListAsync();
        }

        public new async Task AddAsync(ProjectMembership membership)
        {
            await _dbSet.AddAsync(membership);
            await _context.SaveChangesAsync();
        }

        public new async Task UpdateAsync(ProjectMembership membership)
        {
            _dbSet.Update(membership);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserMemberOfProjectAsync(Guid projectId, Guid userId)
        {
            return await _dbSet
                .AnyAsync(pm => pm.ProjectId == projectId && pm.UserId == userId && pm.IsActive);
        }

        public async Task<IEnumerable<User>> GetProjectMembersAsync(Guid projectId)
        {
            return await _dbSet
                .Where(pm => pm.ProjectId == projectId && pm.IsActive)
                .Include(pm => pm.User)
                .Select(pm => pm.User!)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetUserProjectsAsync(Guid userId)
        {
            return await _dbSet
                .Where(pm => pm.UserId == userId && pm.IsActive)
                .Include(pm => pm.Project)
                .Select(pm => pm.Project!)
                .ToListAsync();
        }
    }
}
