using System;

namespace SPRM.Business.DTOs
{
    public class SystemSettingDto
    {
        public Guid Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = "General";
        public DateTime LastModified { get; set; }
        public Guid ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
