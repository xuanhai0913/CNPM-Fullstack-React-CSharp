using System;

namespace SPRM.Data.Entities
{
    public class Milestone
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public MilestoneStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public Project? Project { get; set; }
    }

    public enum MilestoneStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Overdue
    }
}