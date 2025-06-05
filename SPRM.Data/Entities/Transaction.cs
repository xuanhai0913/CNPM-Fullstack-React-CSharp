using System;
using System.ComponentModel.DataAnnotations;

namespace SPRM.Data.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public TransactionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public Guid? ApprovedBy { get; set; }

        public Project? Project { get; set; }
    }

    public enum TransactionStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
