namespace Pubinno.Application.Interfaces.Services
{
    public interface IExceptionLogService
    {
        Task LogAsync(string path, string method, string message, string? stackTrace, int statusCode);
    }
}
