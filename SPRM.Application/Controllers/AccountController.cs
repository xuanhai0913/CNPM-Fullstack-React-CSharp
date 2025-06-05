using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;
using SPRM.Data.Entities;

namespace SPRM.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var result = _accountService.Register(user);
            if (result)
                return Ok("Đăng ký thành công");
            return BadRequest("Đăng ký thất bại");
        }

        // Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _accountService.Login(request.Username, request.Password);
            if (user != null)
                return Ok(new { Message = "Đăng nhập thành công", User = user });
            return Unauthorized("Đăng nhập thất bại");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}