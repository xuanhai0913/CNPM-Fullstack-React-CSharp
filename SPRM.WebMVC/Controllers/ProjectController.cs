using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;
using SPRM.Business.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SPRM.WebMVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return View(projects);
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(ProjectDto projectDto)
        {
            if (ModelState.IsValid)
            {
                // Get current user ID from claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    projectDto.PrincipalInvestigatorId = userId;
                    projectDto.Id = Guid.NewGuid(); // Generate new ID
                    
                    var result = await _projectService.CreateProjectAsync(projectDto);
                    if (result)
                    {
                        TempData["SuccessMessage"] = "Dự án đã được tạo thành công!";
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", "Không thể tạo dự án. Vui lòng thử lại.");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể xác định người dùng hiện tại.");
                }
            }
            return View(projectDto);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProjectDto projectDto)
        {
            if (id != projectDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _projectService.UpdateProjectAsync(projectDto);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Không thể cập nhật dự án. Vui lòng thử lại.");
            }
            return View(projectDto);
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
