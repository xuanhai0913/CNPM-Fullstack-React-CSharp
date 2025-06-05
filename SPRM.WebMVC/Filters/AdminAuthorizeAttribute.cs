using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SPRM.WebMVC.Filters
{
    /// <summary>
    /// Custom authorization filter để kiểm tra quyền Admin
    /// </summary>
    public class AdminAuthorizeAttribute : ActionFilterAttribute
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
            
            // Kiểm tra quyền Admin
            if (userRole != "Administrator" && userRole != "Admin")
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}
