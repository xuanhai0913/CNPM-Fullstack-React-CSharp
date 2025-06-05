using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(SPRMDbContext context) : base(context) { }

        // Implement interface methods explicitly
        public new async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public new async Task AddAsync(Transaction transaction)
        {
            await _dbSet.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public new async Task UpdateAsync(Transaction transaction)
        {
            _dbSet.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetPendingTransactionsAsync()
        {
            return await _dbSet
                .Where(t => t.Status == TransactionStatus.Pending)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByProjectIdAsync(Guid projectId)
        {
            return await _dbSet
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }
    }
}