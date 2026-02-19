using AutoMapper;
using MediatR;
using Pubinno.Application.Common.Models;
using Pubinno.Application.Interfaces.Repositories;
using Pubinno.Domain.Entities;

namespace Pubinno.Application.Features.Pours.Commands.RecordPour
{

    public class RecordPourCommandHandler : IRequestHandler<RecordPourRequest, ApiResponse<bool>>
    {
        private readonly IPourRepository _pourRepository;
        private readonly IMapper _mapper;

        public RecordPourCommandHandler(IPourRepository pourRepository, IMapper mapper)
        {
            _pourRepository = pourRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(RecordPourRequest request, CancellationToken cancellationToken)
        {
            var exists = await _pourRepository.ExistsByEventIdAsync(request.EventId);
            if (exists)
                return new ApiResponse<bool> { Success = true, Data = false, Message = "Already recorded" };

            var pourEvent = _mapper.Map<PourEvent>(request);

            await _pourRepository.AddAsync(pourEvent);

            return new ApiResponse<bool> { Success = true, Data = true, Message = "Created" };
        }
    }

}
