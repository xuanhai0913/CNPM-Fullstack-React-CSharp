using System;

namespace SPRM.Data.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public Project? Project { get; set; }
    }

    public enum TaskStatus
    {
        New,
        InProgress,
        Done,
        Cancelled
    }
}