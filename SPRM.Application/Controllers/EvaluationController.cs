using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;

namespace SPRM.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;

        public EvaluationController(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }
        // Evaluate project
        [HttpPost("evaluate-project")]
        public IActionResult EvaluateProject([FromBody] object projectEvaluationDto)
        {
            // TODO: Đánh giá dự án
            return Ok("Đã đánh giá dự án");
        }
    }
}