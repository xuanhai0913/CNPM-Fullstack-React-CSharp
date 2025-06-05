using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPRM.Data.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid PrincipalInvestigatorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public User? PrincipalInvestigator { get; set; }
        public ICollection<Milestone>? Milestones { get; set; }
        public ICollection<TaskItem>? TaskItems { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<Proposal>? Proposals { get; set; }
        public ICollection<Evaluation>? Evaluations { get; set; }
    }

    public enum ProjectStatus
    {
        Planning,
        Active,
        Completed,
        Cancelled
    }
}
