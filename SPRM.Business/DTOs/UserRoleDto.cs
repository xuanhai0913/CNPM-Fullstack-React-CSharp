using System;

namespace SPRM.Business.DTOs
{
    public class UserRoleDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateTime AssignedDate { get; set; }
        public Guid AssignedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Description { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
