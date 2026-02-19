using Microsoft.Extensions.Logging;
using Pubinno.Application.Interfaces.Services;

namespace Pubinno.Persistence.Repositories
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void Info(string message) => _logger.LogInformation(message);
        public void Debug(string message) => _logger.LogDebug(message);
        public void Error(string message) => _logger.LogError(message);
    }
}
