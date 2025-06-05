using System;

namespace SPRM.Data.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid CreatedBy { get; set; }
        public ReportType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public ReportStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }

        public Project? Project { get; set; }
        public User? Creator { get; set; }
    }

    public enum ReportType
    {
        Progress,
        Financial,
        Final,
        Quarterly,
        Annual,
        Analytics
    }

    public enum ReportStatus
    {
        Draft,
        Submitted,
        UnderReview,
        Approved,
        Rejected,
        Published
    }
}