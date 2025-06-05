using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<Dictionary<string, object>> GetProjectStatisticsAsync(Guid projectId);
        Task<Dictionary<string, object>> GetSystemStatisticsAsync();
    }
}