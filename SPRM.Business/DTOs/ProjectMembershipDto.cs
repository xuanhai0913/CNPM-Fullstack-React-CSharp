using System;

namespace SPRM.Business.DTOs
{
    public class ProjectMembershipDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = "Member";
        public string? Responsibilities { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime? RemovedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }
        
        // For display purposes
        public string? ProjectName { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
    }

    public class CreateProjectMembershipDto
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = "Member";
        public string? Responsibilities { get; set; }
        public string? Notes { get; set; }
    }
}
