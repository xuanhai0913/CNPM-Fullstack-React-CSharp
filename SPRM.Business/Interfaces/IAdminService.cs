using System.Threading.Tasks;

namespace SPRM.Business.Interfaces
{
    public interface IAdminService
    {
        bool ManageUserRoles(object userRoleDto);
        bool ConfigureSystem(object configDto);
        
        // Additional async methods for MVC controllers
        Task<bool> ChangeUserRoleAsync(Guid userId, string newRole);
        Task<bool> UpdateSystemSettingsAsync(object settingsDto);
    }
}
