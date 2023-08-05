namespace LunarLanderGame.Logging
{
    using System.Diagnostics;

    using static LunarLanderGame.Logging.ILogger;


    internal class ConsoleLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            string logLevelString = GetLogLevelString(level);
            Debug.WriteLine($"[{logLevelString}] {message}");
        }

        private string GetLogLevelString(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return "DEBUG";
                case LogLevel.Info:
                    return "INFO";
                case LogLevel.Warning:
                    return "WARNING";
                case LogLevel.Error:
                    return "ERROR";
                case LogLevel.Fatal:
                    return "FATAL";
                default:
                    return "UNKNOWN";
            }
        }
    }

}
