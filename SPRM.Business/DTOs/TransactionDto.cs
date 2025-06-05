using System;

namespace SPRM.Business.DTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = "Expense";
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; } = "Pending";
        public Guid CreatedBy { get; set; }
        public string? Category { get; set; }
        public string? Receipt { get; set; }
    }
}
