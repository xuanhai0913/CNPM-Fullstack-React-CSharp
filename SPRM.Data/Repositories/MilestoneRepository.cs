using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{    public class MilestoneRepository : BaseRepository<Milestone>, IMilestoneRepository
    {
        public MilestoneRepository(SPRMDbContext context) : base(context) { }

        // Implement interface methods explicitly
        public new async Task<Milestone?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public new async Task AddAsync(Milestone milestone)
        {
            await _dbSet.AddAsync(milestone);
            await _context.SaveChangesAsync();
        }

        public new async Task UpdateAsync(Milestone milestone)
        {
            _dbSet.Update(milestone);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Milestone>> GetByProjectIdAsync(Guid projectId)
        {
            return await _dbSet
                .Where(m => m.ProjectId == projectId)
                .ToListAsync();
        }
    }
}