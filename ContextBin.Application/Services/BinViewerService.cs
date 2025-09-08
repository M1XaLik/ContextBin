using ContextBin.Domain.Interfaces;

namespace ContextBin.Application.Services
{
    public class BinViewerService
    {
        private readonly ILogger _logger;
        private readonly IBinViewer _binViewer;

        public BinViewerService(IBinViewer binViewer, ILogger logger)
        {
            _logger = logger;
            _binViewer = binViewer;
        }

        public void ViewBin()
        {
            _logger.Information("Starting bin viewing service...");
            _binViewer.View();
            _logger.Information("Bin viewing service completed.");
        }
    }
}