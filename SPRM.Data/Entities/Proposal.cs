using System;

namespace SPRM.Data.Entities
{
    public class Proposal
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ResearcherId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public ProposalStatus Status { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public Guid? ReviewedBy { get; set; }

        public Project? Project { get; set; }
        public User? Researcher { get; set; }
    }

    public enum ProposalStatus
    {
        Pending,
        Approved,
        Rejected
    }
}