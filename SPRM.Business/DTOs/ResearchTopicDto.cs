using System;

namespace SPRM.Business.DTOs
{
    public class ResearchTopicDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Keywords { get; set; }
        public string? Requirements { get; set; }
    }
}
