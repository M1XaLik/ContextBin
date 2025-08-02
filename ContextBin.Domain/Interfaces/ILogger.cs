namespace ContextBin.Domain.Interfaces
{
    public interface ILogger
    {
        void Information(string message);
        void Error(string message, Exception? exception = null);
        void Warning(string message);
    }
}