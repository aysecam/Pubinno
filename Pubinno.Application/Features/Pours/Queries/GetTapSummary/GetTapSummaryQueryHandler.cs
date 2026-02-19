using MediatR;
using Pubinno.Application.Common.Models;
using Pubinno.Application.Interfaces.Repositories;

namespace Pubinno.Application.Features.Pours.Queries.GetTapSummary
{

    public class GetTapSummaryQueryHandler : IRequestHandler<GetTapSummaryRequest, ApiResponse<TapSummaryResponse>>
    {
        private readonly IPourRepository _pourRepository;

        public GetTapSummaryQueryHandler(IPourRepository pourRepository)
        {
            _pourRepository = pourRepository;
        }

        public async Task<ApiResponse<TapSummaryResponse>> Handle(GetTapSummaryRequest request, CancellationToken cancellationToken)
        {
            var summary = await _pourRepository.GetTapSummaryAsync(request.DeviceId, request.From, request.To);

            return new ApiResponse<TapSummaryResponse> { Success = true, Data = summary, Message = "Success" };
        }
    }

}
