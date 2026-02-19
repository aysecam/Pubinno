using MediatR;
using Pubinno.Application.Common.Models;

namespace Pubinno.Application.Features.Pours.Queries.GetTapSummary
{
    public class GetTapSummaryRequest : IRequest<ApiResponse<TapSummaryResponse>>
    {
        public string DeviceId { get; set; } = null!;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
