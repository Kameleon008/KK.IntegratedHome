namespace KK.IH.Devices.ESP32.Utility.Debug
{
    using System;

    public static class Logger
    {
        public static LogLevel logLevel = LogLevel.Information;

        public static void Trace(string message)
        {
            if (logLevel <= LogLevel.Trace)
            {
                Console.WriteLine($"[Time UTC]:{DateTime.UtcNow.ToString("G")} [Trace]: {message}");
            }
        }

        public static void Debug(string message)
        {
            if (logLevel <= LogLevel.Debug)
            {
                Console.WriteLine($"[Time UTC]:{DateTime.UtcNow.ToString("G")} [Debug]: {message}");
            }
        }

        public static void Info(string message)
        {
            if (logLevel <= LogLevel.Information)
            {
                Console.WriteLine($"[Time UTC]:{DateTime.UtcNow.ToString("G")} [Info]: {message}");
            }
        }

        public static void Warning(string message)
        {
            if (logLevel <= LogLevel.Warning)
            {
                Console.WriteLine($"[Time UTC]:{DateTime.UtcNow.ToString("G")} [Warning]: {message}");
            }
        }

        public static void Error(string message)
        {
            if (logLevel <= LogLevel.Error)
            {
                Console.WriteLine($"[Time UTC]:{DateTime.UtcNow.ToString("G")} [Error]: {message}");
            }
        }

        public static void Critical(string message)
        {
            if (logLevel <= LogLevel.Critical)
            {
                Console.WriteLine($"[Time UTC]:{DateTime.UtcNow.ToString("G")} [Critical]: {message}");
            }
        }

        public static void None(string message)
        {
            if (logLevel <= LogLevel.None)
            {
                Console.WriteLine($"[Time UTC]:{DateTime.UtcNow.ToString("G")} [None]: {message}");
            }
        }

        public enum LogLevel
        {
            Trace = 0,
            Debug = 1,
            Information = 2,
            Warning = 3,
            Error = 4,
            Critical = 5,
            None = 6,
        }
    }
}
