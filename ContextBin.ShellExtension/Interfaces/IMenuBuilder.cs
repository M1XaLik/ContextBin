namespace ContextBin.ShellExtension.Interfaces
{
    public interface IMenuBuilder
    {
        ContextMenuStrip CreateMenu(
            Action View,
            Action Send,
            Action Empty
        );
    }
}