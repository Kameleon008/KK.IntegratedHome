namespace KK.IH.Source.EventHub.Components.Logger
{
    using System;
    using System.Text;
    using System.Threading;
    using Microsoft.Extensions.Logging;

    public static class StaticLogger
    {
        private static readonly LogLevel SelectedLogLevel;

        static StaticLogger()
        {
            SelectedLogLevel = LogLevel.Trace;
        }

        public static void Trace(string message, params object[] parameters)
        {
            LogMessage(LogLevel.Trace, null!, null!, message, parameters);
        }

        public static void Trace(Exception e, string message, params object[] parameters)
        {
            LogMessage(LogLevel.Trace, null!, e, message, parameters);
        }

        public static void Debug(string message, params object[] parameters)
        {
            LogMessage(LogLevel.Debug, null!, null!, message, parameters);
        }

        public static void Debug(Exception e, string message, params object[] parameters)
        {
            LogMessage(LogLevel.Debug, null!, e, message, parameters);
        }

        public static void Information(string message, params object[] parameters)
        {
            LogMessage(LogLevel.Information, null!, null!, message, parameters);
        }

        public static void Information(Exception e, string message, params object[] parameters)
        {
            LogMessage(LogLevel.Information, null!, e, message, parameters);
        }

        public static void Warning(string message, params object[] parameters)
        {
            LogMessage(LogLevel.Warning, null!, null!, message, parameters);
        }

        public static void Warning(Exception e, string message, params object[] parameters)
        {
            LogMessage(LogLevel.Warning, null!, e, message, parameters);
        }

        public static void Error(string message, params object[] parameters)
        {
            LogMessage(LogLevel.Error, null!, null!, message, parameters);
        }

        public static void Error(Exception e, string message, params object[] parameters)
        {
            LogMessage(LogLevel.Error, null!, e, message, parameters);
        }

        public static void Critical(string message, params object[] parameters)
        {
            LogMessage(LogLevel.Critical, null!, null!, message, parameters);
        }

        public static void Critical(Exception e, string message, params object[] parameters)
        {
            LogMessage(LogLevel.Critical, null!, e, message, parameters);
        }

        private static void LogMessage(LogLevel logLevel, object o, Exception e, string message, params object[] parameters)
        {


            if (logLevel < SelectedLogLevel)
            {
                return;
            }

            var headerBuilder = new StringBuilder()
                //.Append($"[ {DateTime.Now.ToString("dd.MM.yyyy_hh:mm:ss.fff")} ]")
                .Append($"{logLevel.ToString()}")
                .Append(" ");

            var messageBuilder = new StringBuilder();

            if (o != null)
            {
                headerBuilder
                    .Append("\"")
                    .Append(o.GetType().FullName)
                    .Append("\"")
                    .Append(" ");
            }

            if (message != null)
            {
                messageBuilder.Append(parameters.Length > 0 ? string.Format(message, parameters) : message);
            }

            if (e != null)
            {
                messageBuilder
                    .Append(" ")
                    .Append(e.Message)
                    .Append(Environment.NewLine)
                    .Append(e.StackTrace);
            }


            Console.ForegroundColor = GetColor(logLevel);
            Console.Write(headerBuilder.ToString());

            Console.ResetColor();
            Console.WriteLine(messageBuilder.ToString());
        }

        private static ConsoleColor GetColor(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.None: return ConsoleColor.White;
                case LogLevel.Trace: return ConsoleColor.Blue;
                case LogLevel.Debug: return ConsoleColor.Yellow;
                case LogLevel.Information: return ConsoleColor.Green;
                case LogLevel.Warning: return ConsoleColor.DarkYellow;
                case LogLevel.Error: return ConsoleColor.DarkRed;
                case LogLevel.Critical: return ConsoleColor.Red;
                default: return ConsoleColor.Red;
            }
        }
    }
}
