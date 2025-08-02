using System.Runtime.InteropServices;
using ContextBin.Domain.Interfaces;

namespace ContextBin.Infrastructure.Implementation
{
    /// <summary>
    /// This class is a concrete implementation of the IBinCleaner interface for the Windows platform.
    /// It uses Platform Invoke (P/Invoke) to call native Windows API functions to empty the recycle bin.
    /// </summary>
    public class BinCleanerImplementation : IBinCleaner
    {
        private readonly ILogger _logger;

        public BinCleanerImplementation(ILogger logger)
        {
            _logger = logger;
        }

        public void Empty()
        {
            _logger.Information("Starting recycle bin cleanup...");

            try
            {
                // These are constants (flags) used to control the behavior of the SHEmptyRecycleBin function.
                // SHERB_NOCONFIRMATION: Prevents the "Are you sure you want to delete these items?" dialog.
                // SHERB_NOPROGRESSUI: Prevents showing a progress bar.
                // SHERB_NOSOUND: Prevents playing the deletion sound.
                const uint SHERB_NOCONFIRMATION = 0x00000001;
                const uint SHERB_NOPROGRESSUI = 0x00000002;
                const uint SHERB_NOSOUND = 0x00000004;

                // Call the Windows API to empty the Recycle Bin.
                // IntPtr.Zero is used for 'hwnd' as there is no specific window to own the operation.
                // 'null' for 'pszRootPath' means to empty all recycle bins on the system.
                SHEmptyRecycleBin(IntPtr.Zero, null, SHERB_NOCONFIRMATION | SHERB_NOPROGRESSUI | SHERB_NOSOUND);

                _logger.Information("Recycle bin cleaned successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while cleaning recycle bin: {ex.Message}");
            }
        }

        /// <summary>
        /// This is a P/Invoke declaration for the SHEmptyRecycleBin function in Shell32.dll.
        /// It acts as a bridge, allowing managed C# code to call the unmanaged Windows API function.
        /// </summary>
        /// <param name="hwnd">A handle to the window that will own any dialog boxes that are displayed.</param>
        /// <param name="pszRootPath">The path to the root drive on which to empty the recycle bin. A value of null empties all bins.</param>
        /// <param name="dwFlags">Flags that control the operation.</param>
        /// <returns>Returns S_OK if successful, or an error code otherwise.</returns>
        [DllImport("Shell32.dll")] // DLL - Dynamic Link Library, Shell32.dll is a Windows system library that contains functions for shell operations.
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string? pszRootPath, uint dwFlags);
    }
}