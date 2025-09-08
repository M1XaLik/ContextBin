using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System.Runtime.InteropServices;
using System.Windows.Forms; //! (DO NOT DELETE) to use ContextMenuStrip
using ContextBin.Domain.Interfaces;
using ContextBin.Application.Services;
using ContextBin.Infrastructure.Configurations;
using ContextBin.Infrastructure.Implementation;

namespace ContextBin.Presentation
{
    /// <summary>
    /// This class provides a context menu extension for the Windows desktop.
    /// It's the entry point for our application from the Windows Shell.
    /// </summary>

    // The attributes below are crucial for registering the COM server (our shell extension) in Windows
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [DisplayName("ContextBin (CleanDesk) Context Menu Extension")]
    [Guid("7697295c-1480-4ef0-8184-c27587a0dbe8")] // A unique identifier for this COM class
    [COMServerAssociation(AssociationType.DesktopBackground)] // Specifies where the context menu will appear

    public class ContextBinContextMenu : SharpContextMenu
    {
        // Use Application Services as dependencies
        // The Presentation layer works with high-level services from the Application layer
        private readonly BinCleanerService _binCleanerService;
        private readonly BinViewerService _binViewerService;

        public ContextBinContextMenu()
        {
            // Composition Root: assemble all dependencies here

            // 1. Create a logger instance
            ContextBin.Domain.Interfaces.ILogger logger = new SerilogLogger();

            // 2. Create concrete implementations of domain interfaces (passing the logger as dependency)
            IBinCleaner windowsCleaner = new BinCleanerImplementation(logger);
            IBinViewer windowsViewer = new BinViewerImplementation(logger);

            // 3. Create the Application services, injecting the concrete implementations
            _binCleanerService = new BinCleanerService(windowsCleaner, logger);
            _binViewerService = new BinViewerService(windowsViewer, logger);
        }

        protected override bool CanShowMenu()
        {
            // The [COMServerAssociation] attribute tells Windows where to "register" your extension.
            // AssociationType.DesktopBackground means your code will be invoked when the user
            // right-clicks on the desktop background
            //
            // The CanShowMenu() method is called by the SharpShell framework after Windows has
            // invoked your code. It provides an opportunity to add extra conditions. For instance,
            // you could check for modifier keys (like Shift) and return 'false' if they aren't
            // pressed, preventing the menu from showing
            return true;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            // This method builds and returns the actual context menu
            var contextMenu = new ContextMenuStrip();
            var mainItem = (ToolStripMenuItem)contextMenu.Items.Add("ContextBin (CleanDesk)");

            var viewBinItem = mainItem.DropDownItems.Add("View Recycle Bin");
            viewBinItem.Click += (sender, args) => _binViewerService.ViewBin();

            var emptyBinItem = mainItem.DropDownItems.Add("Empty Recycle Bin");
            emptyBinItem.Click += (sender, args) => _binCleanerService.CleanBin();

            return contextMenu;
        }
    }
}