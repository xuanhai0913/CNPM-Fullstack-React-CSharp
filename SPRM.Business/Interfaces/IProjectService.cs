using SPRM.Data.Entities;
using SPRM.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPRM.Business.Interfaces
{
    public interface IProjectService
    {
        Project GetProjectInfo(int id);
        IEnumerable<Project> GetAllProjects();
        bool SubmitProposal(Proposal proposal);
        bool UpdateTaskProgress(int taskId, string status);
        string GetReportAndAnalytics(int projectId);
        IEnumerable<Notification> GetNotifications(int userId);
        
        // Async methods for MVC controllers
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectByIdAsync(Guid id);
        Task<bool> CreateProjectAsync(ProjectDto projectDto);
        Task<bool> UpdateProjectAsync(ProjectDto projectDto);
        Task<bool> DeleteProjectAsync(Guid id);
    }
}
