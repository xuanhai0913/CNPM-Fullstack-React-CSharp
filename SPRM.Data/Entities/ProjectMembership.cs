using System;

namespace SPRM.Data.Entities
{
    public class ProjectMembership
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

        // Navigation properties
        public Project? Project { get; set; }
        public User? User { get; set; }
    }

    public enum MembershipRole
    {
        Leader,
        Member,
        Collaborator,
        Observer
    }
}
