using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;

namespace SPRM.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(SPRMDbContext context)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Check if data already exists
            if (await context.Users.AnyAsync())
            {
                return; // Database has been seeded
            }

            // Seed Users
            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@sprm.edu",
                    FullName = "System Administrator",
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Administrator",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "john.doe@university.edu",
                    FullName = "John Doe",
                    Username = "johndoe",
                    Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Role = "Researcher",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "jane.smith@university.edu",
                    FullName = "Jane Smith",
                    Username = "janesmith",
                    Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Role = "Researcher",
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();

            // Seed Research Topics
            var researchTopics = new List<ResearchTopic>
            {
                new ResearchTopic
                {
                    Id = Guid.NewGuid(),
                    Title = "Artificial Intelligence",
                    Description = "Research in machine learning, deep learning, and AI applications",
                    Keywords = "AI, Machine Learning, Deep Learning",
                    CreatedBy = users[0].Id,
                    Status = ResearchTopicStatus.Published,
                    EstimatedBudget = 150000,
                    EstimatedDurationMonths = 12,
                    CreatedAt = DateTime.UtcNow
                },
                new ResearchTopic
                {
                    Id = Guid.NewGuid(),
                    Title = "Climate Change",
                    Description = "Environmental research on climate change impacts and solutions",
                    Keywords = "Climate, Environment, Sustainability",
                    CreatedBy = users[0].Id,
                    Status = ResearchTopicStatus.Published,
                    EstimatedBudget = 200000,
                    EstimatedDurationMonths = 18,
                    CreatedAt = DateTime.UtcNow
                },
                new ResearchTopic
                {
                    Id = Guid.NewGuid(),
                    Title = "Biotechnology",
                    Description = "Research in genetic engineering and biotechnological applications",
                    Keywords = "Biotech, Genetics, Engineering",
                    CreatedBy = users[0].Id,
                    Status = ResearchTopicStatus.Published,
                    EstimatedBudget = 300000,
                    EstimatedDurationMonths = 24,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.ResearchTopics.AddRangeAsync(researchTopics);
            await context.SaveChangesAsync();

            // Seed Projects
            var projects = new List<Project>
            {
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "AI-Powered Student Assessment System",
                    Description = "Development of an intelligent system for automated student assessment using machine learning algorithms.",
                    Budget = 150000,
                    StartDate = DateTime.UtcNow.AddDays(-30),
                    EndDate = DateTime.UtcNow.AddDays(335),
                    Status = ProjectStatus.Active,
                    PrincipalInvestigatorId = users[1].Id,
                    CreatedAt = DateTime.UtcNow
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Sustainable Energy Solutions",
                    Description = "Research on renewable energy technologies and their implementation in urban environments.",
                    Budget = 200000,
                    StartDate = DateTime.UtcNow.AddDays(-60),
                    EndDate = DateTime.UtcNow.AddDays(305),
                    Status = ProjectStatus.Active,
                    PrincipalInvestigatorId = users[2].Id,
                    CreatedAt = DateTime.UtcNow
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Genetic Markers for Disease Prediction",
                    Description = "Identification and analysis of genetic markers for early disease prediction and prevention.",
                    Budget = 300000,
                    StartDate = DateTime.UtcNow.AddDays(30),
                    EndDate = DateTime.UtcNow.AddDays(395),
                    Status = ProjectStatus.Planning,
                    PrincipalInvestigatorId = users[1].Id,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Projects.AddRangeAsync(projects);
            await context.SaveChangesAsync();

            // Seed User Roles
            var userRoles = new List<UserRole>
            {
                new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    Role = "Administrator",
                    AssignedAt = DateTime.UtcNow
                },
                new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = users[1].Id,
                    Role = "Researcher",
                    AssignedAt = DateTime.UtcNow
                },
                new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = users[2].Id,
                    Role = "Researcher",
                    AssignedAt = DateTime.UtcNow
                }
            };

            await context.UserRoles.AddRangeAsync(userRoles);
            await context.SaveChangesAsync();
        }
    }
}
