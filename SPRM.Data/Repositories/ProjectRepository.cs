using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(SPRMDbContext context) : base(context) { }

        // Implement interface methods explicitly
        public new async Task<Project?> GetByIdAsync(Guid projectId)
        {
            return await _dbSet.FindAsync(projectId);
        }

        public new async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public new async Task AddAsync(Project project)
        {
            await _dbSet.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public new async Task UpdateAsync(Project project)
        {
            _dbSet.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>> GetByPIAsync(Guid piId)
        {
            return await _dbSet
                .Where(p => p.PrincipalInvestigatorId == piId)
                .ToListAsync();
        }
    }
}
