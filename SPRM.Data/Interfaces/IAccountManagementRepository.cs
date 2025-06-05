using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface IAccountManagementRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AssignRoleAsync(Guid userId, string role);
        Task UpdateSystemSettingsAsync(SystemSetting settings);
        Task<SystemSetting> GetSettingsAsync();
    }
}