using MediatR;
using Pubinno.Application.Common.Behaviors.Transaction;
using Pubinno.Application.Common.Models;

namespace Pubinno.Application.Features.Pours.Commands.RecordPour
{
    public class RecordPourRequest : IRequest<ApiResponse<bool>>, ITransactionalRequest
    {
        public Guid EventId { get; set; }
        public string DeviceId { get; set; } = null!;
        public string LocationId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public int VolumeMl { get; set; }
    }

  

}
