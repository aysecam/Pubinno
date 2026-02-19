using Microsoft.EntityFrameworkCore;
using Pubinno.Application.Features.Pours.Queries.GetTapSummary;
using Pubinno.Application.Interfaces.Repositories;
using Pubinno.Domain.Entities;
using Pubinno.Persistence.Context;
using System.Threading;

namespace Pubinno.Persistence.Repositories
{
    public class PourRepository : EfRepositoryBase<PourEvent>, IPourRepository
    {
        public PourRepository(PubinnoDbContext context) : base(context) { }

        public async Task<bool> ExistsByEventIdAsync(Guid eventId)
            => await _dbSet.AnyAsync(x => x.EventId == eventId);

        public async Task<List<PourEvent>> GetSummaryDataAsync(string deviceId, DateTime from, DateTime to)
            => await _dbSet
                .Where(x => x.DeviceId == deviceId &&
                            x.StartedAt >= from &&
                            x.StartedAt <= to)
                .ToListAsync();


        public async Task<TapSummaryResponse> GetTapSummaryAsync(string deviceId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            var baseQuery = _dbSet
                .AsNoTracking()
                .Where(x => x.DeviceId == deviceId &&
                            x.StartedAt >= from &&
                            x.StartedAt <= to);

            var byProduct = await baseQuery
                .GroupBy(x => x.ProductId)
                .Select(g => new ProductSummaryDto
                {
                    ProductId = g.Key,
                    VolumeMl = g.Sum(x => x.VolumeMl),
                    Pours = g.Count()
                })
                .OrderByDescending(x => x.VolumeMl)
                .ToListAsync(cancellationToken);

            var byLocation = await baseQuery
                .GroupBy(x => x.LocationId)
                .Select(g => new LocationSummaryDto
                {
                    LocationId = g.Key,
                    VolumeMl = g.Sum(x => x.VolumeMl),
                    Pours = g.Count()
                })
                .OrderByDescending(x => x.VolumeMl)
                .ToListAsync(cancellationToken);

            return new TapSummaryResponse
            {
                DeviceId = deviceId,
                From = from,
                To = to,
                TotalVolumeMl = byProduct.Sum(x => x.VolumeMl),
                TotalPours = byProduct.Sum(x => x.Pours),
                ByProduct = byProduct,
                ByLocation = byLocation,
                TopProduct = byProduct.FirstOrDefault(),
                TopLocation = byLocation.FirstOrDefault()
            };
        }
    }


}
