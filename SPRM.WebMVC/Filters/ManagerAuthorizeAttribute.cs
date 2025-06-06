using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SPRM.WebMVC.Filters
{
    /// <summary>
    /// Custom authorization filter để kiểm tra quyền Manager (Researcher, Staff)
    /// </summary>
    public class ManagerAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRole = context.HttpContext.Session.GetString("UserRole");
            var userId = context.HttpContext.Session.GetString("UserId");
            
            // Kiểm tra đăng nhập
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }
            
            // Kiểm tra quyền Manager (Researcher, Staff có thể quản lý dự án)
            var allowedRoles = new[] { "Administrator", "Admin", "Researcher", "Staff" };
            if (!allowedRoles.Contains(userRole))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}
