using Microsoft.AspNetCore.Mvc;
using SPRM.Business.Interfaces;

namespace SPRM.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HostInstitutionController : ControllerBase
    {
        private readonly IHostInstitutionService _hostInstitutionService;

        public HostInstitutionController(IHostInstitutionService hostInstitutionService)
        {
            _hostInstitutionService = hostInstitutionService;
        }
        // Approve PI
        [HttpPost("approve-pi")]
        public IActionResult ApprovePI([FromBody] object piDto)
        {
            // TODO: Duyệt chủ nhiệm đề tài
            return Ok("Đã duyệt chủ nhiệm đề tài");
        }
    }
}