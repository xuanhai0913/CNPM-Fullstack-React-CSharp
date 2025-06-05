using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        Task<IEnumerable<UserRole>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<UserRole>> GetByRoleAsync(string role);
    }
}
