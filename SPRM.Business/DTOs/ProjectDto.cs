using System;

namespace SPRM.Business.DTOs
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid PrincipalInvestigatorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PriorityLevel { get; set; } = "Medium";
        public string ProjectCategory { get; set; } = "Research";
        public string FieldOfStudy { get; set; } = "IT";
    }

    public class CreateProjectDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid PrincipalInvestigatorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public string PriorityLevel { get; set; } = "Medium";
        public string ProjectCategory { get; set; } = "Research";
        public string FieldOfStudy { get; set; } = "IT";
    }
}
