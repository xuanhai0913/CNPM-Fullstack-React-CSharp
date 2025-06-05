using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface IEvaluationRepository
    {
        Task<Evaluation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Evaluation>> GetByProjectIdAsync(Guid projectId);
        Task AddAsync(Evaluation evaluation);
        Task UpdateAsync(Evaluation evaluation);
    }
}
