namespace SimpleFileLogging
{
    using System.Collections.Generic;

    /// <summary>
    /// Simple Logger for file logging.
    /// </summary>
    public partial class SimpleFileLogger
    {
        /// <summary>
        /// Logs info to info log file.
        /// </summary>
        /// <param name="messages">Messages to log</param>
        public static void Info(params string[] messages)
        {
            Log(SimpleLogType.Info, messages);
        }

        /// <summary>
        /// Logs info dictionary to log file.
        /// </summary>
        /// <param name="dictionary">Dictionary for logging</param>
        public static void Info(Dictionary<string, string> dictionary)
        {
            Log(SimpleLogType.Info, dictionary);
        }
    }
}