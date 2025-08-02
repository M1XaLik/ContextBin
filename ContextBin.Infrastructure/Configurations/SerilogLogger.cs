using Serilog;

namespace ContextBin.Infrastructure.Configurations
{
    public class SerilogLogger: ContextBin.Domain.Interfaces.ILogger
    {
        private readonly Serilog.Core.Logger _logger;

        public SerilogLogger()
        {
            // Створюємо конфігурацію Serilog
            _logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    "contextbin.log",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
        }
        
        public void Information(string message)
        {
            _logger.Information(message);
        }

        public void Error(string message, Exception? exception = null)
        {
            if (exception != null)
            {
                _logger.Error(exception, message);
            }
            else
            {
                _logger.Error(message);
            }
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }
    }
}