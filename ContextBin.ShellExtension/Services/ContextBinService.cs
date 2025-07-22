using ContextBin.ShellExtension.Interfaces;

namespace ContextBin.ShellExtension.Services
{
    public class ContextBinService: IContextBinService
    {
        // TODO: ПОТРІБНО РЕАЛІЗУВАТИ МЕТОДИ
        public void SendSelectedToBin()
        {
            // Логіка для відправки вибраних файлів/папок до ContextBin
            Console.WriteLine("Selected items sent to ContextBin.");
        }

        public void ViewBin()
        {
            // Логіка для перегляду вмісту ContextBin
            Console.WriteLine("Viewing ContextBin.");
        }

        public void EmptyBin()
        {
            // Логіка для очищення ContextBin
            Console.WriteLine("ContextBin emptied.");
        }
    }
}