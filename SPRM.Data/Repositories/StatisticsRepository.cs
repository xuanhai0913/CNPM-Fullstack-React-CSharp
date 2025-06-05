using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPRM.Data.Entities;
using SPRM.Data.Interfaces;

namespace SPRM.Data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly SPRMDbContext _context;

        public StatisticsRepository(SPRMDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, object>> GetProjectStatisticsAsync(Guid projectId)
        {
            var project = await _context.Projects
                .Include(p => p.Milestones)
                .Include(p => p.TaskItems)
                .Include(p => p.Transactions)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
                return new Dictionary<string, object>();

            return new Dictionary<string, object>
            {
                { "TotalMilestones", project.Milestones?.Count ?? 0 },
                { "CompletedMilestones", project.Milestones?.Count(m => m.Status == MilestoneStatus.Completed) ?? 0 },
                { "TotalTasks", project.TaskItems?.Count ?? 0 },
                { "CompletedTasks", project.TaskItems?.Count(t => t.Status == Entities.TaskStatus.Done) ?? 0 },
                { "TotalBudget", project.Budget },
                { "SpentAmount", project.Transactions?.Where(t => t.Status == TransactionStatus.Approved).Sum(t => t.Amount) ?? 0 }
            };
        }

        public async Task<Dictionary<string, object>> GetSystemStatisticsAsync()
        {
            var totalProjects = await _context.Projects.CountAsync();
            var activeProjects = await _context.Projects.CountAsync(p => p.Status == ProjectStatus.Active);
            var totalUsers = await _context.Users.CountAsync();
            var totalProposals = await _context.Proposals.CountAsync();
            var pendingProposals = await _context.Proposals.CountAsync(p => p.Status == ProposalStatus.Pending);

            return new Dictionary<string, object>
            {
                { "TotalProjects", totalProjects },
                { "ActiveProjects", activeProjects },
                { "TotalUsers", totalUsers },
                { "TotalProposals", totalProposals },
                { "PendingProposals", pendingProposals }
            };
        }
    }
}
