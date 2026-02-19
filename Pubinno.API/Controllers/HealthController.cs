using Microsoft.AspNetCore.Mvc;
using Pubinno.Application.Interfaces.Services;

namespace Pubinno.API.Controllers
{
    [Route("health")]
    public class HealthController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public HealthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Health()
        {
            try
            {
                await _unitOfWork.CanConnectAsync();
                return Ok(new { status = "ok", db = "ok" });
            }
            catch
            {
                return StatusCode(503, new { status = "error", db = "error" });
            }
        }
    }
}
