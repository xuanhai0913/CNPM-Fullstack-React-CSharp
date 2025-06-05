using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;

namespace SPRM.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        // Approve transactions
        [HttpPost("approve-transaction")]
        public IActionResult ApproveTransaction([FromBody] object transactionDto)
        {
            // TODO: Duyệt giao dịch
            return Ok("Đã duyệt giao dịch");
        }

        // Manage system account
        [HttpPost("manage-account")]
        public IActionResult ManageSystemAccount([FromBody] object accountDto)
        {
            // TODO: Quản lý tài khoản hệ thống
            return Ok("Đã quản lý tài khoản hệ thống");
        }

        // Create research topic
        [HttpPost("create-topic")]
        public IActionResult CreateResearchTopic([FromBody] object topicDto)
        {
            // TODO: Tạo đề tài nghiên cứu
            return Ok("Đã tạo đề tài nghiên cứu");
        }
    }
}