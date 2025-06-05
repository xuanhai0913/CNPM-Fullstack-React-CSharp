using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;
using SPRM.Business.DTOs;

namespace SPRM.WebMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAccountService _accountService;

        public AdminController(IAdminService adminService, IAccountService accountService)
        {
            _adminService = adminService;
            _accountService = accountService;
        }

        // GET: Admin
        public IActionResult Index()
        {
            // Check if user is admin
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // GET: Admin/ManageUsers
        public async Task<IActionResult> ManageUsers()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            var users = await _accountService.GetAllUsersAsync();
            return View(users);
        }

        // POST: Admin/ChangeUserRole
        [HttpPost]
        public async Task<IActionResult> ChangeUserRole(Guid userId, string newRole)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return Json(new { success = false, message = "Không có quyền thực hiện" });
            }

            var result = await _adminService.ChangeUserRoleAsync(userId, newRole);
            if (result)
            {
                return Json(new { success = true, message = "Đã cập nhật vai trò người dùng" });
            }

            return Json(new { success = false, message = "Không thể cập nhật vai trò" });
        }

        // GET: Admin/SystemSettings
        public IActionResult SystemSettings()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Admin/UpdateSystemSettings
        [HttpPost]
        public async Task<IActionResult> UpdateSystemSettings(SystemSettingsDto settingsDto)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return Json(new { success = false, message = "Không có quyền thực hiện" });
            }

            if (ModelState.IsValid)
            {
                var result = await _adminService.UpdateSystemSettingsAsync(settingsDto);
                if (result)
                {
                    return Json(new { success = true, message = "Đã cập nhật cài đặt hệ thống" });
                }
            }

            return Json(new { success = false, message = "Không thể cập nhật cài đặt" });
        }

        // GET: Admin/Reports
        public IActionResult Reports()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
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
