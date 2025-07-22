using Serilog;

namespace ContextBin.ShellExtension.Utils
{
    public static class LoggerFactory
    {
        public static ILogger CreateLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                // .WriteTo.File("logs/contextbin.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // * тип ILogger повинен обов'язково щось повернути
            return Log.Logger;
        }
    }
}