using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetPendingTransactionsAsync();
        Task<IEnumerable<Transaction>> GetByProjectIdAsync(Guid projectId);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);

    }
}