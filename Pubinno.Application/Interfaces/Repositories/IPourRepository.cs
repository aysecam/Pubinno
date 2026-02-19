using Pubinno.Application.Features.Pours.Queries.GetTapSummary;
using Pubinno.Domain.Entities;

namespace Pubinno.Application.Interfaces.Repositories
{
    public interface IPourRepository : IRepository<PourEvent>
    {
        Task<bool> ExistsByEventIdAsync(Guid eventId);
        Task<TapSummaryResponse> GetTapSummaryAsync(string deviceId, DateTime from, DateTime to, CancellationToken cancellationToken = default);

    }
}
