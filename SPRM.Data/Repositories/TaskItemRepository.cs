using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{    public class TaskItemRepository : BaseRepository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(SPRMDbContext context) : base(context) { }

        // Implement interface methods explicitly
        public new async Task<TaskItem?> GetByIdAsync(Guid taskItemId)
        {
            return await _dbSet.FindAsync(taskItemId);
        }

        public new async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public new async Task AddAsync(TaskItem taskItem)
        {
            await _dbSet.AddAsync(taskItem);
            await _context.SaveChangesAsync();
        }

        public new async Task UpdateAsync(TaskItem taskItem)
        {
            _dbSet.Update(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId)
        {
            return await _dbSet
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }
    }
}