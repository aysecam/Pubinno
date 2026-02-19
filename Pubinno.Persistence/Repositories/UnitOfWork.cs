using Pubinno.Application.Interfaces.Services;
using Pubinno.Persistence.Context;

namespace Pubinno.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PubinnoDbContext _context;

        public UnitOfWork(PubinnoDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CanConnectAsync() => await _context.Database.CanConnectAsync();
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);
    }
}
