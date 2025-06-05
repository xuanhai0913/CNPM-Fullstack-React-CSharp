using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface ISystemSettingRepository : IBaseRepository<SystemSetting>
    {
        Task<SystemSetting?> GetByKeyAsync(string key);
        Task<Dictionary<string, string>> GetAllSettingsAsync();
    }
}
