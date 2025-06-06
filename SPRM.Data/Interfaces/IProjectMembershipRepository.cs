using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface IProjectMembershipRepository
    {
        Task<ProjectMembership?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProjectMembership>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<ProjectMembership>> GetByUserIdAsync(Guid userId);
        Task<ProjectMembership?> GetByProjectAndUserAsync(Guid projectId, Guid userId);
        Task<IEnumerable<ProjectMembership>> GetAllAsync();
        Task AddAsync(ProjectMembership membership);
        Task UpdateAsync(ProjectMembership membership);
        Task DeleteAsync(Guid id);
        Task<bool> IsUserMemberOfProjectAsync(Guid projectId, Guid userId);
        Task<IEnumerable<User>> GetProjectMembersAsync(Guid projectId);
        Task<IEnumerable<Project>> GetUserProjectsAsync(Guid userId);
    }
}
