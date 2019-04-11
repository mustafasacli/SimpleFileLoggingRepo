namespace SimpleFileLogging
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Simple Logger for file logging.
    /// </summary>
    public partial class SimpleFileLogger
    {
        /// <summary>
        /// Logs exception details to error log file.
        /// </summary>
        /// <param name="e">Exception to log</param>
        public static void LogError(Exception e)
        {
            if (e == null)
                return;

            var list = new List<string>
            {
                $"Message : {e.Message}",
                $"Stack Trace : {e.StackTrace}",
                $"Exception Data : {e.ToString()}",
            };

            Log(SimpleLogType.Error, list.ToArray());
        }

        /// <summary>
        /// Log Messages to error log files.
        /// </summary>
        /// <param name="messages">Messages to log</param>
        public static void LogError(params string[] messages)
        {
            Log(SimpleLogType.Debug, messages);
        }
    }
}