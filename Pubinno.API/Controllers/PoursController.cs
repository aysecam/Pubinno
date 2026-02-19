using Microsoft.AspNetCore.Mvc;
using Pubinno.Application.Features.Pours.Commands.RecordPour;
using Pubinno.Application.Features.Pours.Queries.GetTapSummary;

namespace Pubinno.API.Controllers
{
    [Route("v1/pours")]
    public class PoursController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> RecordPour([FromBody] RecordPourRequest request)
        {
            var result = await Mediator.Send(request);

            if (!result.Data)
                return Ok(result);

            return StatusCode(201, result);
        }

        [HttpGet("/v1/taps/{deviceId}/summary")]
        public async Task<IActionResult> GetTapSummary(
        string deviceId,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
        {
            var request = new GetTapSummaryRequest
            {
                DeviceId = deviceId,
                From = from,
                To = to
            };

            var result = await Mediator.Send(request);
            return Ok(result);
        }
    }
}
