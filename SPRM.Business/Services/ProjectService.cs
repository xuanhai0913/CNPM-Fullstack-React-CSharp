using SPRM.Data.Entities;
using SPRM.Data.Interfaces;
using SPRM.Business.Interfaces;
using SPRM.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPRM.Business.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProposalRepository _proposalRepository;
        private readonly ITaskItemRepository _taskRepository;

        public ProjectService(
            IProjectRepository projectRepository,
            IProposalRepository proposalRepository,
            ITaskItemRepository taskRepository)
        {
            _projectRepository = projectRepository;
            _proposalRepository = proposalRepository;
            _taskRepository = taskRepository;
        }

        public Project GetProjectInfo(int id)
        {
            // Convert int to Guid for now - should be updated to use Guid throughout
            var guidId = Guid.NewGuid(); // This is temporary
            var project = _projectRepository.GetByIdAsync(guidId).Result;
            return project ?? new Project(); // Return an empty project object instead of null
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _projectRepository.GetAllAsync().Result;
        }

        public bool SubmitProposal(Proposal proposal)
        {
            _proposalRepository.AddAsync(proposal);
            return true;
        }

        public bool UpdateTaskProgress(int taskId, string status)
        {
            // TODO: Update task status
            return true;
        }

        public string GetReportAndAnalytics(int projectId)
        {
            return "Project analytics report";
        }

        public IEnumerable<Notification> GetNotifications(int userId)
        {
            // TODO: Get notifications for user
            return new List<Notification>();
        }

        // New async methods for MVC controllers
        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Status = p.Status.ToString(),
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Budget = p.Budget,
                PrincipalInvestigatorId = p.PrincipalInvestigatorId
            });
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            
            if (project == null) return null;
            
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status.ToString(),
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Budget = project.Budget,
                PrincipalInvestigatorId = project.PrincipalInvestigatorId
            };
        }

        public async Task<bool> CreateProjectAsync(ProjectDto projectDto)
        {
            if (!Enum.TryParse<ProjectStatus>(projectDto.Status, out var status))
            {
                status = ProjectStatus.Planning; // Default status
            }

            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = projectDto.Name,
                Description = projectDto.Description,
                Status = status,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                Budget = projectDto.Budget,
                PrincipalInvestigatorId = projectDto.PrincipalInvestigatorId,
                CreatedAt = DateTime.UtcNow
            };

            await _projectRepository.AddAsync(project);
            return true;
        }

        public async Task<bool> UpdateProjectAsync(ProjectDto projectDto)
        {
            var project = await _projectRepository.GetByIdAsync(projectDto.Id);
            if (project == null) return false;

            if (Enum.TryParse<ProjectStatus>(projectDto.Status, out var status))
            {
                project.Status = status;
            }

            project.Name = projectDto.Name;
            project.Description = projectDto.Description;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;
            project.Budget = projectDto.Budget;
            project.UpdatedAt = DateTime.UtcNow;

            await _projectRepository.UpdateAsync(project);
            return true;
        }

        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            
            if (project == null) return false;
            
            await _projectRepository.DeleteAsync(project.Id);
            return true;
        }
    }
}
