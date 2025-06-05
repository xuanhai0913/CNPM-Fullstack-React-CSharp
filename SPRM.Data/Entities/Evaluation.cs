using System;

namespace SPRM.Data.Entities
{
    public class Evaluation
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid EvaluatedBy { get; set; }
        public EvaluationType Type { get; set; }
        public EvaluationResult Result { get; set; }
        public int Score { get; set; } // 0-100
        public string Comment { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
        public DateTime EvaluatedAt { get; set; }
        public EvaluationStatus Status { get; set; }

        public Project? Project { get; set; }
        public User? Evaluator { get; set; }
    }

    public enum EvaluationType
    {
        Project,
        Milestone,
        Final,
        MidTerm
    }

    public enum EvaluationResult
    {
        Excellent,
        Good,
        Satisfactory,
        NeedsImprovement,
        Unsatisfactory
    }

    public enum EvaluationStatus
    {
        Draft,
        Submitted,
        Approved,
        Rejected
    }
}