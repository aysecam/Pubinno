using Pubinno.Application.Interfaces.Services;
using Pubinno.Domain.Entities;
using Pubinno.Persistence.Context;

namespace Pubinno.Persistence.Repositories
{
    public class ExceptionLogRepository : IExceptionLogService
    {
        private readonly PubinnoDbContext _context;

        public ExceptionLogRepository(PubinnoDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string path, string method, string message, string? stackTrace, int statusCode)
        {
            await _context.ExceptionLogs.AddAsync(new ExceptionLog
            {
                Path = path,
                Method = method,
                Message = message,
                StackTrace = stackTrace,
                StatusCode = statusCode,
                CreatedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
    }
}
