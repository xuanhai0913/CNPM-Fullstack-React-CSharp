using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class ResearchTopicRepository : BaseRepository<ResearchTopic>, IResearchTopicRepository
    {
        public ResearchTopicRepository(SPRMDbContext context) : base(context) { }

        // Implement interface methods explicitly
        public new async Task<ResearchTopic?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public new async Task<IEnumerable<ResearchTopic>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public new async Task AddAsync(ResearchTopic topic)
        {
            await _dbSet.AddAsync(topic);
            await _context.SaveChangesAsync();
        }

        public new async Task UpdateAsync(ResearchTopic topic)
        {
            _dbSet.Update(topic);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResearchTopic>> GetByCreatorIdAsync(Guid userId)
        {
            return await _dbSet
                .Where(r => r.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ResearchTopic>> GetByStatusAsync(ResearchTopicStatus status)
        {
            return await _dbSet
                .Where(r => r.Status == status)
                .ToListAsync();
        }
    }
}