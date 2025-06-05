using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class SystemSettingRepository : BaseRepository<SystemSetting>, ISystemSettingRepository
{
    public SystemSettingRepository(SPRMDbContext context) : base(context) {}

    public async Task<SystemSetting?> GetByKeyAsync(string key)
    {
        return await _context.SystemSettings
            .FirstOrDefaultAsync(s => s.Key == key);
    }

    public async Task<Dictionary<string, string>> GetAllSettingsAsync()
    {
        var settings = await _context.SystemSettings.ToListAsync();
        return settings.ToDictionary(s => s.Key, s => s.Value);
    }
}

}