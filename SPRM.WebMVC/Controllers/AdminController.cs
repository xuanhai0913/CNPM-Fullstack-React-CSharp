using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;
using SPRM.Business.DTOs;
using SPRM.WebMVC.Filters;
using SPRM.Data;

namespace SPRM.WebMVC.Controllers
{
    [AdminAuthorize] // Áp dụng phân quyền Admin cho toàn bộ controller
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAccountService _accountService;
        private readonly SPRMDbContext _context;

        public AdminController(IAdminService adminService, IAccountService accountService, SPRMDbContext context)
        {
            _adminService = adminService;
            _accountService = accountService;
            _context = context;
        }

        // GET: Admin
        public IActionResult Index()
        {
            ViewBag.WelcomeMessage = $"Chào mừng quản trị viên: {HttpContext.Session.GetString("FullName")}";
            return View();
        }

        // GET: Admin/ManageUsers
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _accountService.GetAllUsersAsync();
            return View(users);
        }

        // POST: Admin/ChangeUserRole
        [HttpPost]
        public async Task<IActionResult> ChangeUserRole(Guid userId, string newRole)
        {
            // Kiểm tra role hợp lệ
            var validRoles = new[] { "Administrator", "Researcher", "Student", "Staff" };
            if (!validRoles.Contains(newRole))
            {
                return Json(new { success = false, message = "Vai trò không hợp lệ." });
            }

            var result = await _adminService.ChangeUserRoleAsync(userId, newRole);
            if (result)
            {
                return Json(new { success = true, message = $"Đã thay đổi vai trò người dùng thành {newRole}." });
            }

            return Json(new { success = false, message = "Không thể cập nhật vai trò." });
        }

        // GET: Admin/SystemSettings
        public IActionResult SystemSettings()
        {
            return View();
        }

        // POST: Admin/UpdateSystemSettings
        [HttpPost]
        public async Task<IActionResult> UpdateSystemSettings(SystemSettingsDto settingsDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.UpdateSystemSettingsAsync(settingsDto);
                if (result)
                {
                    TempData["Success"] = "Đã cập nhật cài đặt hệ thống thành công.";
                    return RedirectToAction("SystemSettings");
                }
                ModelState.AddModelError("", "Không thể cập nhật cài đặt hệ thống.");
            }
            return View("SystemSettings", settingsDto);
        }

        // GET: Admin/Reports
        public IActionResult Reports()
        {
            return View();
        }

        // GET: Admin/DatabaseManagement
        public async Task<IActionResult> DatabaseManagement()
        {
            ViewBag.HasSampleData = await DatabaseCleaner.HasSampleDataAsync(_context);
            return View();
        }

        // POST: Admin/ClearDatabase  
        [HttpPost]
        public async Task<IActionResult> ClearDatabase()
        {
            try
            {
                await DatabaseCleaner.ClearSampleDataAsync(_context);
                await DatabaseCleaner.EnsureAdminUserAsync(_context);
                
                TempData["Success"] = "Đã xóa dữ liệu mẫu thành công. Admin user được giữ lại.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Không thể xóa dữ liệu: {ex.Message}";
            }
            
            return RedirectToAction("DatabaseManagement");
        }
    }

    public class SystemSettingsDto
    {
        public string SystemName { get; set; } = string.Empty;
        public string SystemDescription { get; set; } = string.Empty;
        public int MaxProjectsPerUser { get; set; }
        public int MaxFileSize { get; set; }
        public bool AllowGuestAccess { get; set; }
    }
}
