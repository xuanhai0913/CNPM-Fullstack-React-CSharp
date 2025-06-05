using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;

namespace SPRM.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        // View report and statistics
        [HttpGet("statistics")]
        public IActionResult ViewReportAndStatistics()
        {
            // TODO: Lấy báo cáo và thống kê
            return Ok("Báo cáo và thống kê hệ thống");
        }
    }
}