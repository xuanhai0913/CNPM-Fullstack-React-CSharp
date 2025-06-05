using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;
using SPRM.Business.DTOs;

namespace SPRM.WebMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.LoginAsync(loginDto.Username, loginDto.Password);
                if (user != null)
                {
                    // Store user info in session or cookie
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("UserName", user.Username);
                    HttpContext.Session.SetString("UserRole", user.Role);
                    
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            }
            return View(loginDto);
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.RegisterAsync(userDto);
                if (result)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Không thể đăng ký tài khoản. Vui lòng thử lại.");
            }
            return View(userDto);
        }

        // POST: Account/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Profile
        public async Task<IActionResult> Profile()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login");
            }

            var user = await _accountService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: Account/EditProfile
        public async Task<IActionResult> EditProfile()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login");
            }

            var user = await _accountService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // POST: Account/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserDto userDto)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                userDto.Id = userId;
                var result = await _accountService.UpdateUserAsync(userDto);
                if (result)
                {
                    return RedirectToAction("Profile");
                }
                ModelState.AddModelError("", "Không thể cập nhật thông tin. Vui lòng thử lại.");
            }
            return View(userDto);
        }
    }

    public class LoginDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
