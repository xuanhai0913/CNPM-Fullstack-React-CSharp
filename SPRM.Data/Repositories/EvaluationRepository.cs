using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class EvaluationRepository : BaseRepository<Evaluation>, IEvaluationRepository
    {
        public EvaluationRepository(SPRMDbContext context) : base(context) { }

        public async Task<IEnumerable<Evaluation>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Evaluations
                .Where(e => e.ProjectId == projectId)
                .OrderByDescending(e => e.EvaluatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Evaluation>> GetByEvaluatorAsync(Guid evaluatorId)
        {
            return await _context.Evaluations
                .Where(e => e.EvaluatedBy == evaluatorId)
                .OrderByDescending(e => e.EvaluatedAt)
                .ToListAsync();
        }
    }
}
