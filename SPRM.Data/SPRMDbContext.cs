using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;

namespace SPRM.Data
{
    public class SPRMDbContext : DbContext
    {
        public SPRMDbContext(DbContextOptions<SPRMDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ResearchTopic> ResearchTopics { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Project>()
                .HasOne(p => p.PrincipalInvestigator)
                .WithMany()
                .HasForeignKey(p => p.PrincipalInvestigatorId);

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Project)
                .WithMany()
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Project)
                .WithMany()
                .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Researcher)
                .WithMany()
                .HasForeignKey(p => p.ResearcherId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Project)
                .WithMany()
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<Milestone>()
                .HasOne(m => m.Project)
                .WithMany()
                .HasForeignKey(m => m.ProjectId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId);

            // Configure decimal precision
            modelBuilder.Entity<Project>()
                .Property(p => p.Budget)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            // Configure string lengths
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .HasMaxLength(200);
        }
    }
}
