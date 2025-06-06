using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class ProposalRepository : BaseRepository<Proposal>, IProposalRepository
    {
        public ProposalRepository(SPRMDbContext context) : base(context) { }

        // Implement interface methods explicitly
        public new async Task<Proposal?> GetByIdAsync(Guid proposalId)
        {
            return await _dbSet.FindAsync(proposalId);
        }

        public new async Task AddAsync(Proposal proposal)
        {
            await _dbSet.AddAsync(proposal);
            await _context.SaveChangesAsync();
        }

        public new async Task UpdateAsync(Proposal proposal)
        {
            _dbSet.Update(proposal);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Proposal>> GetByResearcherIdAsync(Guid researcherId)
        {
            return await _dbSet
                .Where(p => p.ResearcherId == researcherId)
                .ToListAsync();
        }        public async Task<IEnumerable<Proposal>> GetPendingProposalsAsync()
        {
            return await _dbSet
                .Where(p => p.Status == ProposalStatus.Pending)
                .ToListAsync();
        }

        public new async Task<IEnumerable<Proposal>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
