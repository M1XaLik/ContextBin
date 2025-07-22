using System.Runtime.InteropServices;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using ContextBin.ShellExtension.Interfaces;

namespace ContextBin.ShellExtension;

// Ці атрибути реєструють клас як Shell Extension у системі.
// ComVisible(true) дозволяє COM-інтерфейсам (які Windows використовує для Shell Extensions) бачити твій клас.
// ClassInterfaceType.None означає, що COM не генеруватиме автоматичний інтерфейс.
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
// DisplayName - це назва, яка відображатиметься в інструментах реєстрації (наприклад, SharpShell.Installer).
[DisplayName("ContextBin (CleanDesk) Context Menu Extension")]

// ATTENTION
// Guid - це УНІКАЛЬНИЙ ідентифікатор розширення.
[Guid("88cb321c-8dd0-4481-acba-917a35afa3ac")]

// COMServerAssociation вказує, для яких типів об'єктів буде з'являтися твоє меню.
[COMServerAssociation(AssociationType.AllFilesAndFolders)]
// DirectoryBackground = для фону папок (коли правий клік у порожньому місці папки).
[COMServerAssociation(AssociationType.DirectoryBackground)]
// DesktopBackground = для фону робочого столу.
[COMServerAssociation(AssociationType.DesktopBackground)]

// SharpContextMenu є базовим класом для створення контекстних меню в SharpShell.
public class ContextBinContextMenu : SharpContextMenu
{
    private readonly IMenuBuilder _menuBuilder;
    private readonly IContextBinService _contextBinService;

    public ContextBinContextMenu(IMenuBuilder menuBuilder, IContextBinService contextBinService)
    {
        _menuBuilder = menuBuilder;
        _contextBinService = contextBinService;
    }

    protected override bool CanShowMenu() => true;

    // Цей метод викликається, коли користувач відкриває контекстне меню.
    protected override ContextMenuStrip CreateMenu()
    {
        // Використовуємо MenuBuilder для створення меню.
        return _menuBuilder.CreateMenu
        (
            _contextBinService.ViewBin,
            _contextBinService.SendSelectedToBin,
            _contextBinService.EmptyBin
        );
    }
}
