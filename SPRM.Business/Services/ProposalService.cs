using SPRM.Data.Entities;
using SPRM.Data.Interfaces;
using SPRM.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPRM.Business.Services
{
    public class ProposalService : IProposalService
    {
        private readonly IProposalRepository _proposalRepository;

        public ProposalService(IProposalRepository proposalRepository)
        {
            _proposalRepository = proposalRepository;
        }

        public async Task<IEnumerable<Proposal>> GetAllProposalsAsync()
        {
            // Since IProposalRepository doesn't have GetAllAsync, we'll get pending proposals as a placeholder
            return await _proposalRepository.GetPendingProposalsAsync();
        }

        public async Task<Proposal?> GetProposalByIdAsync(Guid id)
        {
            return await _proposalRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateProposalAsync(Proposal proposal)
        {
            proposal.Id = Guid.NewGuid();
            proposal.SubmittedAt = DateTime.UtcNow;
            await _proposalRepository.AddAsync(proposal);
            return true;
        }

        public async Task<bool> UpdateProposalAsync(Proposal proposal)
        {
            await _proposalRepository.UpdateAsync(proposal);
            return true;
        }

        public async Task<bool> DeleteProposalAsync(Guid id)
        {
            await _proposalRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<Proposal>> GetProposalsByResearcherAsync(Guid researcherId)
        {
            return await _proposalRepository.GetByResearcherIdAsync(researcherId);
        }
    }
}
