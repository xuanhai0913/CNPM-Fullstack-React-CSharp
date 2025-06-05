using System;

namespace SPRM.Business.DTOs
{
    public class EvaluationDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid EvaluatorId { get; set; }
        public string Criteria { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public string Comments { get; set; } = string.Empty;
        public DateTime EvaluationDate { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
