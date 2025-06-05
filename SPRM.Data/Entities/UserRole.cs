using System;

namespace SPRM.Data.Entities
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateTime AssignedAt { get; set; }

        public User? User { get; set; }
    }
}
