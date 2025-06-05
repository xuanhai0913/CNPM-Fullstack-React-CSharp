using System;

namespace SPRM.Business.DTOs
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Type { get; set; } = "Progress";
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string Status { get; set; } = "Draft";
        public string? FilePath { get; set; }
    }
}
