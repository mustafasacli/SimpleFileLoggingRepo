namespace SimpleFileLogging
{
    using System.Collections.Generic;

    /// <summary>
    /// Simple Logger for file logging.
    /// </summary>
    public partial class SimpleFileLogger
    {
        /// <summary>
        /// Logs debug info to debug log file.
        /// </summary>
        /// <param name="messages">Messages to log</param>
        public void Debug(params string[] messages)
        {
            Log(SimpleLogType.Debug, messages);
        }

        /// <summary>
        /// Logs debug info dictionary to debug log file.
        /// </summary>
        /// <param name="dictionary">Dictionary for logging</param>
        public void Debug(Dictionary<string, string> dictionary)
        {
            Log(SimpleLogType.Debug, dictionary);
        }
    }
}