namespace LunarLanderGame.Logging
{
    internal interface ILogger
    {
        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error,
            Fatal
        }


        void Log(LogLevel level, string message);

    }
}
