using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;
using SPRM.Business.DTOs;
using SPRM.WebMVC.Filters;
using SPRM.Data;
using Microsoft.EntityFrameworkCore;

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

        // GET: Admin/CreateUser
        public IActionResult CreateUser()
        {
            return View();
        }

        // POST: Admin/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _accountService.RegisterAsync(userDto);
                    if (result != null)
                    {
                        TempData["Success"] = $"Đã tạo người dùng {userDto.Username} thành công.";
                        return RedirectToAction("ManageUsers");
                    }
                    ModelState.AddModelError("", "Không thể tạo người dùng.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                }
            }
            return View(userDto);
        }

        // GET: Admin/DetailUser/5
        public async Task<IActionResult> DetailUser(Guid id)
        {
            var user = await _accountService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Admin/UpdateUser/5
        public async Task<IActionResult> UpdateUser(Guid id)
        {
            var user = await _accountService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/UpdateUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(Guid id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _accountService.UpdateUserAsync(userDto);
                    if (result)
                    {
                        TempData["Success"] = "Đã cập nhật thông tin người dùng thành công.";
                        return RedirectToAction("ManageUsers");
                    }
                    ModelState.AddModelError("", "Không thể cập nhật thông tin người dùng.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                }
            }
            return View(userDto);
        }

        // POST: Admin/RemoveUser/5
        [HttpPost]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            try
            {
                var result = await _accountService.DeleteUserAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Đã xóa người dùng thành công." });
                }
                return Json(new { success = false, message = "Không thể xóa người dùng." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        // GET: Admin/ImportUser
        public IActionResult ImportUser()
        {
            return View();
        }

        // POST: Admin/ImportUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportUser(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                ModelState.AddModelError("", "Vui lòng chọn file CSV.");
                return View();
            }

            try
            {
                var importedCount = await ProcessUserImportFile(csvFile);
                TempData["Success"] = $"Đã import thành công {importedCount} người dùng.";
                return RedirectToAction("ManageUsers");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi import file: {ex.Message}");
                return View();
            }
        }

        // GET: Admin/SendNotification
        public IActionResult SendNotification()
        {
            ViewBag.Users = _context.Users.Select(u => new { u.Id, u.FullName, u.Username }).ToList();
            return View();
        }

        // POST: Admin/SendNotification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendNotification(NotificationDto notificationDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Send notification logic here
                    await CreateNotificationAsync(notificationDto);
                    TempData["Success"] = "Đã gửi thông báo thành công.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi gửi thông báo: {ex.Message}");
                }
            }
            ViewBag.Users = _context.Users.Select(u => new { u.Id, u.FullName, u.Username }).ToList();
            return View(notificationDto);
        }

        // Helper method to process CSV import
        private async Task<int> ProcessUserImportFile(IFormFile csvFile)
        {
            using var reader = new StreamReader(csvFile.OpenReadStream());
            var importedCount = 0;
            var line = await reader.ReadLineAsync(); // Skip header

            while ((line = await reader.ReadLineAsync()) != null)
            {
                var values = line.Split(',');
                if (values.Length >= 4)
                {
                    var userDto = new CreateUserDto
                    {
                        Username = values[0].Trim(),
                        FullName = values[1].Trim(),
                        Email = values[2].Trim(),
                        Role = values[3].Trim(),
                        Password = "DefaultPassword123!" // Should be changed on first login
                    };

                    var result = await _accountService.RegisterAsync(userDto);
                    if (result != null)
                    {
                        importedCount++;
                    }
                }
            }

            return importedCount;
        }

        // Helper method to create notification
        private async Task CreateNotificationAsync(NotificationDto notificationDto)
        {
            var notification = new SPRM.Data.Entities.Notification
            {
                Id = Guid.NewGuid(),
                UserId = notificationDto.UserId,
                Title = notificationDto.Title,
                Message = notificationDto.Message,
                Type = SPRM.Data.Entities.NotificationType.Info,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
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
