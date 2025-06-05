using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;

namespace SPRM.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        // Xem thông tin dự án
        [HttpGet("{id}")]
        public IActionResult ViewProjectInfo(int id)
        {
            // TODO: Lấy thông tin dự án theo id
            return Ok($"Thông tin dự án {id}");
        }

        // Gửi đề xuất (Submit Proposals)
        [HttpPost("submit-proposal")]
        public IActionResult SubmitProposal([FromBody] object proposalDto)
        {
            // TODO: Xử lý gửi đề xuất
            return Ok("Đã gửi đề xuất");
        }

        // Cập nhật tiến độ task (Update task progress)
        [HttpPost("update-task-progress")]
        public IActionResult UpdateTaskProgress([FromBody] object taskProgressDto)
        {
            // TODO: Cập nhật tiến độ task
            return Ok("Đã cập nhật tiến độ task");
        }

        // Xem báo cáo và phân tích (View report and analytics)
        [HttpGet("report")]
        public IActionResult ViewReportAndAnalytics()
        {
            // TODO: Lấy báo cáo và phân tích
            return Ok("Báo cáo và phân tích dự án");
        }

        // Nhận thông báo (Receive Notifications)
        [HttpGet("notifications")]
        public IActionResult ReceiveNotifications()
        {
            // TODO: Lấy danh sách thông báo
            return Ok(new[] { "Thông báo 1", "Thông báo 2" });
        }
    }
}
