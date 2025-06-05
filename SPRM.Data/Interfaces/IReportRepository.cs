using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        Task<IEnumerable<Report>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<Report>> GetByTypeAsync(ReportType type);
        Task<IEnumerable<Report>> GetByStatusAsync(ReportStatus status);
    }
}
