using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(Guid projectId);
        Task<IEnumerable<Project>> GetByPIAsync(Guid piId);
        Task<IEnumerable<Project>> GetAllAsync();
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Guid id);
    }
}
