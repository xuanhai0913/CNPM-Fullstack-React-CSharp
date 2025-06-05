using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid taskItemId);
        Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task AddAsync(TaskItem taskItem);
        Task UpdateAsync(TaskItem taskItem);
        Task DeleteAsync(Guid taskItemId);
    }
}