using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;
using SPRM.Business.DTOs;
using SPRM.WebMVC.Filters;
using SPRM.Data.Interfaces;
using SPRM.Data.Entities;

namespace SPRM.WebMVC.Controllers
{
    [ManagerAuthorize]
    public class ManagerController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IProjectMembershipRepository _projectMembershipRepository;
        private readonly IProposalRepository _proposalRepository;

        public ManagerController(
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            ITaskItemRepository taskItemRepository,
            IProjectMembershipRepository projectMembershipRepository,
            IProposalRepository proposalRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _taskItemRepository = taskItemRepository;
            _projectMembershipRepository = projectMembershipRepository;
            _proposalRepository = proposalRepository;
        }

        // GET: Manager
        public async Task<IActionResult> Index()
        {
            ViewBag.WelcomeMessage = $"Chào mừng quản lý: {HttpContext.Session.GetString("FullName")}";
            
            // Get dashboard statistics
            var totalProjects = await _projectRepository.GetAllAsync();
            var projectCount = totalProjects?.Count() ?? 0;
            var activeProjects = totalProjects?.Where(p => p.Status == ProjectStatus.InProgress).Count() ?? 0;
            var completedProjects = totalProjects?.Where(p => p.Status == ProjectStatus.Completed).Count() ?? 0;
            
            var allTasks = await _taskItemRepository.GetAllAsync();
            var completedTasks = allTasks?.Where(t => t.Status == SPRM.Data.Entities.TaskStatus.Done).Count() ?? 0;
            
            var recentActivities = totalProjects?.Take(5).Select(p => new {
                Title = p.Name,
                Date = p.UpdatedAt ?? p.CreatedAt,
                Type = "Project Updated"
            }).Cast<object>().ToList() ?? new List<object>();

            ViewBag.TotalProjects = projectCount;
            ViewBag.ActiveProjects = activeProjects;
            ViewBag.CompletedProjects = completedProjects;
            ViewBag.CompletedTasks = completedTasks;
            ViewBag.RecentActivities = recentActivities;

            return View();
        }

        // GET: Manager/ManageProjects
        public async Task<IActionResult> ManageProjects()
        {
            var projects = await _projectRepository.GetAllAsync();
            return View(projects);
        }

        // GET: Manager/ProjectDetail/5
        public async Task<IActionResult> ProjectDetail(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            // For now, we'll use empty lists since the repository methods don't exist yet
            ViewBag.ProjectMembers = new List<object>();
            ViewBag.ProjectTasks = new List<object>();

            return View(project);
        }

        // GET: Manager/ManageMembers
        public async Task<IActionResult> ManageMembers()
        {
            var members = await _userRepository.GetAllAsync();
            var filteredMembers = members?.Where(u => u.Role == "Researcher" || u.Role == "Staff" || u.Role == "Student") ?? new List<User>();
            return View(filteredMembers);
        }

        // GET: Manager/AssignMember/5
        public async Task<IActionResult> AssignMember(Guid projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var availableMembers = await _userRepository.GetAllAsync();
            var filteredMembers = availableMembers?.Where(u => u.Role == "Researcher" || u.Role == "Staff" || u.Role == "Student") ?? new List<User>();

            ViewBag.Project = project;
            ViewBag.AvailableMembers = filteredMembers;
            ViewBag.CurrentMembers = new List<object>();

            return View();
        }

        // POST: Manager/AssignMember
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignMember(Guid projectId, Guid memberId, string role = "Member")
        {
            try
            {
                // Check if assignment already exists
                var existingAssignment = await _projectMembershipRepository.GetByProjectAndUserAsync(projectId, memberId);

                if (existingAssignment != null)
                {
                    return Json(new { success = false, message = "Người dùng đã được phân công cho dự án này." });
                }

                // Create new project membership
                var membership = new ProjectMembership
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    UserId = memberId,
                    Role = role,
                    AssignedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _projectMembershipRepository.AddAsync(membership);

                return Json(new { success = true, message = "Đã phân công thành viên thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        // POST: Manager/RemoveMember
        [HttpPost]
        public async Task<IActionResult> RemoveMember(Guid projectId, Guid memberId)
        {
            try
            {
                var membership = await _projectMembershipRepository.GetByProjectAndUserAsync(projectId, memberId);

                if (membership != null)
                {
                    await _projectMembershipRepository.DeleteAsync(membership.Id);
                    return Json(new { success = true, message = "Đã xóa thành viên khỏi dự án." });
                }

                return Json(new { success = false, message = "Không tìm thấy thành viên trong dự án." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        // GET: Manager/SendNotification
        public async Task<IActionResult> SendNotification()
        {
            var members = await _userRepository.GetAllAsync();
            var filteredMembers = members?.Where(u => u.Role == "Researcher" || u.Role == "Staff" || u.Role == "Student")
                                       .Select(u => new { u.Id, u.FullName, u.Username }).ToList();
            ViewBag.Members = filteredMembers != null ? filteredMembers.Cast<object>().ToList() : new List<object>();
            return View();
        }

        // POST: Manager/SendNotification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendNotification(string title, string message, List<Guid> recipients)
        {
            try
            {
                // In a real application, you would send notifications here
                // For now, we'll just show a success message
                TempData["Success"] = "Đã gửi thông báo thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Lỗi gửi thông báo: {ex.Message}";
                return RedirectToAction("SendNotification");
            }
        }

        // GET: Manager/Notifications
        public async Task<IActionResult> Notifications()
        {
            // In a real application, you would fetch notifications from database
            var notifications = new List<object>();
            return View(notifications);
        }

        // GET: Manager/HandleRequests
        public async Task<IActionResult> HandleRequests()
        {
            // This would typically show pending project requests, membership requests, etc.
            var pendingRequests = await _proposalRepository.GetAllAsync();
            var pendingList = pendingRequests?.Where(p => p.Status == ProposalStatus.Pending)
                                           .OrderByDescending(p => p.SubmittedAt)
                                           .ToList() ?? new List<Proposal>();

            return View(pendingList);
        }

        // POST: Manager/ApproveRequest
        [HttpPost]
        public async Task<IActionResult> ApproveRequest(Guid requestId)
        {
            try
            {
                var proposal = await _proposalRepository.GetByIdAsync(requestId);
                if (proposal != null)
                {
                    proposal.Status = ProposalStatus.Approved;
                    proposal.ReviewedAt = DateTime.UtcNow;
                    await _proposalRepository.UpdateAsync(proposal);
                    
                    return Json(new { success = true, message = "Đã phê duyệt yêu cầu thành công." });
                }
                return Json(new { success = false, message = "Không tìm thấy yêu cầu." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        // POST: Manager/RejectRequest
        [HttpPost]
        public async Task<IActionResult> RejectRequest(Guid requestId)
        {
            try
            {
                var proposal = await _proposalRepository.GetByIdAsync(requestId);
                if (proposal != null)
                {
                    proposal.Status = ProposalStatus.Rejected;
                    proposal.ReviewedAt = DateTime.UtcNow;
                    await _proposalRepository.UpdateAsync(proposal);
                    
                    return Json(new { success = true, message = "Đã từ chối yêu cầu." });
                }
                return Json(new { success = false, message = "Không tìm thấy yêu cầu." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }
    }
}
