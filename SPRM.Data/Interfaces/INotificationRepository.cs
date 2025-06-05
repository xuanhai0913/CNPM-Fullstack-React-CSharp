using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPRM.Data.Entities;

namespace SPRM.Data.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Notification notification);
        Task MarkAsReadAsync(Guid notificationId);
    }
}