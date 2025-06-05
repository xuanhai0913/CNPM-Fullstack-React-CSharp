using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;

namespace SPRM.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        // Monitor tasks
        [HttpGet("monitor")]
        public IActionResult MonitorTasks()
        {
            // TODO: Lấy danh sách task cần theo dõi
            return Ok("Danh sách task đang theo dõi");
        }

        // Evaluate milestones
        [HttpPost("evaluate-milestone")]
        public IActionResult EvaluateMilestone([FromBody] object milestoneDto)
        {
            // TODO: Đánh giá milestone
            return Ok("Đã đánh giá milestone");
        }
    }
}