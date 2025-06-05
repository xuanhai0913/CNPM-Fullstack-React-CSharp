using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;

namespace SPRM.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        // Manage user roles
        [HttpPost("manage-user-roles")]
        public IActionResult ManageUserRoles([FromBody] object userRoleDto)
        {
            // TODO: Quản lý vai trò người dùng
            return Ok("Đã cập nhật vai trò người dùng");
        }

        // Configure system settings
        [HttpPost("configure-system")]
        public IActionResult ConfigureSystem([FromBody] object configDto)
        {
            // TODO: Cấu hình hệ thống
            return Ok("Đã cấu hình hệ thống");
        }
    }
}