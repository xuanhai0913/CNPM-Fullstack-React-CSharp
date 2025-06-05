using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface IResearchTopicRepository
    {
        Task<ResearchTopic?> GetByIdAsync(Guid id);
        Task<IEnumerable<ResearchTopic>> GetAllAsync();
        Task AddAsync(ResearchTopic topic);
        Task UpdateAsync(ResearchTopic topic);
        Task DeleteAsync(Guid id);
    }
}