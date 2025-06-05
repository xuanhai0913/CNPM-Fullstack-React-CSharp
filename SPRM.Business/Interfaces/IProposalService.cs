using SPRM.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPRM.Business.Interfaces
{
    public interface IProposalService
    {
        Task<IEnumerable<Proposal>> GetAllProposalsAsync();
        Task<Proposal?> GetProposalByIdAsync(System.Guid id);
        Task<bool> CreateProposalAsync(Proposal proposal);
        Task<bool> UpdateProposalAsync(Proposal proposal);
        Task<bool> DeleteProposalAsync(System.Guid id);
        Task<IEnumerable<Proposal>> GetProposalsByResearcherAsync(System.Guid researcherId);
    }
}
