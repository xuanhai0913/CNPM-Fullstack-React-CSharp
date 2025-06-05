using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        public ReportRepository(SPRMDbContext context) : base(context) { }

        public async Task<IEnumerable<Report>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Reports
                .Where(r => r.ProjectId == projectId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetByTypeAsync(ReportType type)
        {
            return await _context.Reports
                .Where(r => r.Type == type)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetByStatusAsync(ReportStatus status)
        {
            return await _context.Reports
                .Where(r => r.Status == status)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }
    }
}
