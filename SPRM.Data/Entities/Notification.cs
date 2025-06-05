using System;

namespace SPRM.Data.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }

        public User? User { get; set; }
    }

    public enum NotificationType
    {
        Info,
        Warning,
        Success,
        Error
    }
}