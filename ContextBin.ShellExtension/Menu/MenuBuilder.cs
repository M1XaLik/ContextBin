using ContextBin.ShellExtension.Interfaces;

namespace ContextBin.ShellExtension.Menu
{
    public class MenuBuilder : IMenuBuilder
    {
        public ContextMenuStrip CreateMenu(Action View, Action Send, Action Empty)
        {
            // Створюємо головне меню ContextBin:
            var contextMenu = new ContextMenuStrip();

            // Головний пункт "ContextBin":
            var mainItem = (ToolStripMenuItem)contextMenu.Items.Add("ContextBin");

            // Підменю "Відправити в кошик":
            var sendToTrashItem = mainItem.DropDownItems.Add("Відправити в кошик");
            sendToTrashItem.Click += (sender, args) => Send();

            // Підменю "Переглянути кошик":
            var viewTrashItem = mainItem.DropDownItems.Add("Переглянути кошик");
            viewTrashItem.Click += (sender, args) => View();

            // Підменю "Очистити кошик":
            var clearTrashItem = mainItem.DropDownItems.Add("Очистити кошик");
            clearTrashItem.Click += (sender, args) => Empty();

            return contextMenu;
        }
    }
}