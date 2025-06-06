using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{    public interface IProposalRepository
    {
        Task<Proposal?> GetByIdAsync(Guid proposalId);
        Task<IEnumerable<Proposal>> GetByResearcherIdAsync(Guid researcherId);
        Task<IEnumerable<Proposal>> GetPendingProposalsAsync();
        Task<IEnumerable<Proposal>> GetAllAsync();
        Task AddAsync(Proposal proposal);
        Task UpdateAsync(Proposal proposal);
        Task DeleteAsync(Guid id);
    }
}
