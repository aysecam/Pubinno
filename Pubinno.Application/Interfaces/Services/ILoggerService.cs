namespace Pubinno.Application.Interfaces.Services
{
    public interface ILoggerService
    {
        void Info(string message);
        void Debug(string message);
        void Error(string message);

    }
}
