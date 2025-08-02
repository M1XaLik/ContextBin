using System.Diagnostics;
using ContextBin.Domain.Interfaces;

namespace ContextBin.Infrastructure.Implementation
{
    /// <summary>
    /// This class provides an implementation of IBinViewer for Windows.
    /// It's responsible for opening the default Windows Recycle Bin folder.
    /// </summary>
    public class BinViewerImplementation : IBinViewer
    {
        private readonly ILogger _logger;

        public BinViewerImplementation(ILogger logger)
        {
            _logger = logger;
        }

        public void View()
        {
            _logger.Information("Attempting to open the Windows Recycle Bin.");

            try
            {
                // Launch the special Windows folder: "Shell:RecycleBinFolder"
                // Це стандартний механізм для доступу до системних папок.
                Process.Start("explorer.exe", "Shell:RecycleBinFolder");
                _logger.Information("Windows Recycle Bin successfully opened.");
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to open the Windows Recycle Bin.", ex);
            }
        }
    }
}