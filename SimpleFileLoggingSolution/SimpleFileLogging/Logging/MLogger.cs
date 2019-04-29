namespace SimpleFileLogging.Logging
{
    using SimpleFileLogging.Interfaces;
    using System;
    using System.Collections.Generic;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A logger. </summary>
    ///
    /// <remarks>   Msacli, 29.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class MLogger
    {
        private static ISimpleLogger logger;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Static constructor. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        static MLogger()
        {
            logger = SimpleFileLogger.Instance;
        }

        /// <summary>
        /// Enables Method Grouping if it is true
        /// AssemblyFolder\ClassName\MethodName\Error-Info-Debug\logfile. else AssemblyFolder\Error-Info-Debug\ClassName\MethodName\logfile.
        /// </summary>
        public static bool EnableMethodGrouping
        {
            get
            { return logger.EnableMethodGrouping; }
            set
            { logger.EnableMethodGrouping = value; }
        }

        /// <summary>
        /// Logs debug info dictionary to debug log file.
        /// </summary>
        /// <param name="dictionary">Dictionary for logging</param>
        public static void Debug(Dictionary<string, string> dictionary)
        {
            logger.Debug(dictionary);
        }

        /// <summary>
        /// Logs debug info to debug log file.
        /// </summary>
        /// <param name="messages">Messages to log</param>
        public static void Debug(params string[] messages)
        {
            logger.Debug(messages);
        }

        /// <summary>
        /// Log Messages to error log files.
        /// </summary>
        /// <param name="messages">Messages to log</param>
        public static void Error(params string[] messages)
        {
            logger.Error(messages);
        }

        /// <summary>
        /// Logs exception details to error log file.
        /// </summary>
        /// <param name="e">Exception to log</param>
        /// <param name="messages">messages to write.</param>
        public static void Error(Exception e, params string[] messages)
        {
            logger.Error(e, messages);
        }

        /// <summary>
        /// Logs info dictionary to log file.
        /// </summary>
        /// <param name="dictionary">Dictionary for logging</param>
        public static void Info(Dictionary<string, string> dictionary)
        {
            logger.Info(dictionary);
        }

        /// <summary>
        /// Logs info to info log file.
        /// </summary>
        /// <param name="messages">Messages to log</param>
        public static void Info(params string[] messages)
        {
            logger.Info(messages);
        }

        /// <summary>
        /// Log Messages to error log files.
        /// </summary>
        /// <param name="messages">Messages to log</param>
        public static void LogError(params string[] messages)
        {
            logger.LogError(messages);
        }

        /// <summary>
        /// Logs exception details to error log file.
        /// </summary>
        /// <param name="e">Exception to log</param>
        public static void LogError(Exception e)
        {
            logger.LogError(e);
        }
    }
}