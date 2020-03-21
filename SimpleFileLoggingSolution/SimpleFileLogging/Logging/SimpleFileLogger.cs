namespace SimpleFileLogging
{
    using Enums;
    using Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Simple Logger for file logging.
    /// </summary>
    public partial class SimpleFileLogger : ISimpleLogger
    {
        private static Lazy<ISimpleLogger> lazyLogger =
            new Lazy<ISimpleLogger>(() =>
        {
            return new SimpleFileLogger();
        });

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the instance. </summary>
        ///
        /// <value> The instance. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static ISimpleLogger Instance
        { get { return lazyLogger.Value; } }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the type of the simple log date format. </summary>
        ///
        /// <value> The type of the simple log date format. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public SimpleLogDateFormats LogDateFormatType
        { get; set; } = SimpleLogDateFormats.Second;

        /// <summary>
        /// Enables Method Grouping if it is true
        /// AssemblyFolder\ClassName\MethodName\Error-Info-Debug\logfile. else AssemblyFolder\Error-Info-Debug\ClassName\MethodName\logfile.
        /// </summary>
        public bool EnableMethodGrouping
        { get; set; } = false;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor that prevents a default instance of this class from being created.
        /// </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private SimpleFileLogger()
        {
        }

        /// <summary>
        /// Logs exception details to error log file.
        /// </summary>
        /// <param name="e">Exception to log</param>
        public void LogError(Exception e)
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
        public void LogError(params string[] messages)
        {
            Log(SimpleLogType.Error, messages);
        }

        /// <summary>
        /// Logs exception details to error log file.
        /// </summary>
        /// <param name="e">Exception to log</param>
        /// <param name="messages">messages to write.</param>
        public void Error(Exception e, params string[] messages)
        {
            if (e == null)
                return;

            var list = new List<string>
            {
                $"Message : {e.Message}",
                $"Stack Trace : {e.StackTrace}",
                $"Exception Data : {e.ToString()}",
            };

            if (messages != null && messages.Length > 0)
            {
                list.AddRange(messages);
            }

            Log(SimpleLogType.Error, list.ToArray());
        }

        /// <summary>
        /// Log Messages to error log files.
        /// </summary>
        /// <param name="messages">Messages to log</param>
        public void Error(params string[] messages)
        {
            Log(SimpleLogType.Error, messages);
        }

        internal void WriteException(Exception exception)
        {
            try
            {
                if (exception == null)
                    return;

                var rows = new List<string>();

                Exception ex = exception;

                do
                {
                    rows.Add("Message: " + ex.Message);
                    rows.Add("StackTrace: " + ex.StackTrace);
                    rows.Add("HResult: " + ex.HResult.ToString());
                    rows.Add("Source: " + ex.Source);
                    ex = ex.InnerException;

                    if (ex != null)
                        rows.Add("****************");

                } while (ex != null);

                if (rows.Count > 0)
                    rows.Add(AppLoggingValues.Lines);

                var directoryName = AppLoggingValues.AssemblyDirectory;
                string fileFullName = string.Format("{0}/simple-log-error.log", directoryName);
                SimpleFileOperator.Instance.Write(fileFullName, rows);
            }
            catch (Exception)
            { }
        }
    }
}