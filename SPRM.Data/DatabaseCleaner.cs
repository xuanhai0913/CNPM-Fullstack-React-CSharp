using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;

namespace SPRM.Data
{
    public static class DatabaseCleaner
    {
        /// <summary>
        /// Xóa tất cả dữ liệu mẫu khỏi database
        /// </summary>
        public static async Task ClearSampleDataAsync(SPRMDbContext context)
        {
            try
            {
                // Xóa theo thứ tự để tránh lỗi foreign key constraints
                
                // 1. Xóa UserRoles trước
                var userRoles = await context.UserRoles.ToListAsync();
                if (userRoles.Any())
                {
                    context.UserRoles.RemoveRange(userRoles);
                }

                // 2. Xóa Projects và related data
                var projects = await context.Projects.ToListAsync();
                if (projects.Any())
                {
                    context.Projects.RemoveRange(projects);
                }

                // 3. Xóa ResearchTopics
                var researchTopics = await context.ResearchTopics.ToListAsync();
                if (researchTopics.Any())
                {
                    context.ResearchTopics.RemoveRange(researchTopics);
                }

                // 4. Xóa Notifications
                var notifications = await context.Notifications.ToListAsync();
                if (notifications.Any())
                {
                    context.Notifications.RemoveRange(notifications);
                }

                // 5. Xóa TaskItems
                var taskItems = await context.TaskItems.ToListAsync();
                if (taskItems.Any())
                {
                    context.TaskItems.RemoveRange(taskItems);
                }

                // 6. Xóa Milestones
                var milestones = await context.Milestones.ToListAsync();
                if (milestones.Any())
                {
                    context.Milestones.RemoveRange(milestones);
                }

                // 7. Xóa Proposals
                var proposals = await context.Proposals.ToListAsync();
                if (proposals.Any())
                {
                    context.Proposals.RemoveRange(proposals);
                }

                // 8. Xóa Evaluations
                var evaluations = await context.Evaluations.ToListAsync();
                if (evaluations.Any())
                {
                    context.Evaluations.RemoveRange(evaluations);
                }

                // 9. Xóa Reports
                var reports = await context.Reports.ToListAsync();
                if (reports.Any())
                {
                    context.Reports.RemoveRange(reports);
                }

                // 10. Xóa Transactions
                var transactions = await context.Transactions.ToListAsync();
                if (transactions.Any())
                {
                    context.Transactions.RemoveRange(transactions);
                }

                // 11. Cuối cùng xóa Users (trừ admin)
                var usersToDelete = await context.Users
                    .Where(u => u.Role != "Administrator" && u.Role != "Admin")
                    .ToListAsync();
                
                if (usersToDelete.Any())
                {
                    context.Users.RemoveRange(usersToDelete);
                }

                // Lưu tất cả thay đổi
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa dữ liệu mẫu: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Kiểm tra xem database có dữ liệu mẫu hay không
        /// </summary>
        public static async Task<bool> HasSampleDataAsync(SPRMDbContext context)
        {
            var userCount = await context.Users.CountAsync();
            var projectCount = await context.Projects.CountAsync();
            var topicCount = await context.ResearchTopics.CountAsync();
            
            return userCount > 0 || projectCount > 0 || topicCount > 0;
        }

        /// <summary>
        /// Tạo một admin user mặc định nếu chưa có
        /// </summary>
        public static async Task EnsureAdminUserAsync(SPRMDbContext context)
        {
            var adminExists = await context.Users
                .AnyAsync(u => u.Role == "Administrator" || u.Role == "Admin");

            if (!adminExists)
            {
                var adminUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@sprm.edu",
                    FullName = "System Administrator",
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Administrator",
                    CreatedAt = DateTime.UtcNow
                };

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
