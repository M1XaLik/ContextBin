using System.Runtime.InteropServices;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;

using System.Drawing;
using System.Windows.Forms;

namespace ContextBin.ShellExtension;

// Ці атрибути реєструють твій клас як Shell Extension у системі.
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
public class ContextBinContextMenu : SharpContextMenu
{
    protected override bool CanShowMenu()
    {
        // Ми хочемо показувати головний пункт ContextBin завжди, коли це можливо:
        // - Коли вибрані файли/папки (для "Відправити в кошик")
        // - Коли клік по фону папки або робочого столу (для "Переглянути кошик", "Очистити кошик")
        return true; // SharpShell з атрибутами [COMServerAssociation] сам вирішить, де показувати.
    }

    protected override ContextMenuStrip CreateMenu()
    {
        // Головне меню ContextBin:
        var contextMenu = new ContextMenuStrip();

        // Головний пункт "ContextBin":
        var mainItem = (ToolStripMenuItem)contextMenu.Items.Add("ContextBin");

        // Підменю "Відправити в кошик":
        var sendToTrashItem = mainItem.DropDownItems.Add("Відправити в кошик");
        sendToTrashItem.Click += (sender, args) => SendSelectedToContextBin();

        // Підменю "Переглянути кошик":
        var viewTrashItem = mainItem.DropDownItems.Add("Переглянути кошик");
        viewTrashItem.Click += (sender, args) => ViewContextBin();

        // Підменю "Очистити кошик":
        var clearTrashItem = mainItem.DropDownItems.Add("Очистити кошик");
        clearTrashItem.Click += (sender, args) => EmptyContextBin();

        return contextMenu;
    }

    // --- Методи-заглушки для логіки ---

    private void SendSelectedToContextBin()
    {
        // Цей метод буде викликати функцію Windows API для відправки файлів у системний кошик.
        // Поки що заглушка:
        MessageBox.Show($"Відправлення {SelectedItemPaths.Count()} елементів у кошик. (Буде реалізовано)", "ContextBin (CleanDesk)", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ViewContextBin()
    {
        // Цей метод буде запускати наш ContextBin.UI.exe.
        // Поки що заглушка:
        MessageBox.Show("Запуск вікна ContextBin для перегляду та відновлення файлів. (Буде реалізовано)", "ContextBin (CleanDesk)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        // Приклад запуску: Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ContextBin.UI.exe"));
    }

    private void EmptyContextBin()
    {
        // Цей метод буде викликати функцію Windows API для очищення системного кошика.
        // Поки що заглушка:
        MessageBox.Show("Очищення системного кошика. (Буде реалізовано)", "ContextBin (CleanDesk)", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
