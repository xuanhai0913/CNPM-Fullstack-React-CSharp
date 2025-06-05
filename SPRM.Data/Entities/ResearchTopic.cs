using System;

namespace SPRM.Data.Entities
{
    public class ResearchTopic
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
        public ResearchTopicStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal EstimatedBudget { get; set; }
        public int EstimatedDurationMonths { get; set; }

        public User? Creator { get; set; }
    }

    public enum ResearchTopicStatus
    {
        Draft,
        Published,
        Assigned,
        Archived
    }
}