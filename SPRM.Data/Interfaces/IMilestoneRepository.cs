using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface IMilestoneRepository
    {
        Task<Milestone?> GetByIdAsync(Guid id);
        Task<IEnumerable<Milestone>> GetByProjectIdAsync(Guid projectId);
        Task AddAsync(Milestone milestone);
        Task UpdateAsync(Milestone milestone);
        Task DeleteAsync(Guid id);
    }
}
