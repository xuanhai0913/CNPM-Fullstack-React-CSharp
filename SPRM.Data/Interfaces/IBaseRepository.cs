using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPRM.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
}