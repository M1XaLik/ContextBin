using ContextBin.Domain.Interfaces;

namespace ContextBin.Application.Services
{
    public class BinCleanerService
    {
        private readonly ILogger _logger;
        private readonly IBinCleaner _binCleaner;

        public BinCleanerService(IBinCleaner binCleaner, ILogger logger)
        {
            _binCleaner = binCleaner;
            _logger = logger;
        }

        public void CleanBin()
        {
            _logger.Information("Starting bin cleanup service...");
            _binCleaner.Empty();
            _logger.Information("Bin cleanup service completed.");
        }
    }
}