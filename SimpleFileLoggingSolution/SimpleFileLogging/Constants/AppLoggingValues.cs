////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	AppLoggingValues.cs
//
// summary:	Implements the application logging values class
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace SimpleFileLogging
{
    using System;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An application logging values. </summary>
    ///
    /// <remarks>   Msacli, 29.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class AppLoggingValues
    {
        /// <summary>   The log file name format. </summary>
        internal static readonly string LogFileNameFormat = "event-{0}.log";

        /// <summary>   The error log file name format. </summary>
        internal static readonly string ErrorLogFileNameFormat = "error-{0}.log";

        /// <summary>   The ınfo log file name format. </summary>
        internal static readonly string InfoLogFileNameFormat = "info-{0}.log";

        /// <summary>   The debug log file name format. </summary>
        internal static readonly string DebugLogFileNameFormat = "debug-{0}.log";

        /// <summary>   The log file date format. </summary>
        internal static readonly string LogFileDateFormat = "yyyy-MM-dd-HH-mm-ss";

        /// <summary>   The error file date format. </summary>
        internal static readonly string ErrorFileDateFormat = "yyyy-MM-dd-HH-mm-ss";

        /// <summary>   The general date format. </summary>
        internal static readonly string GeneralDateFormat = "yyyy-MM-dd, HH:mm:ss ffffff";

        /// <summary>   The lines. </summary>
        internal static readonly string Lines = "----------------------------------";

        /// <summary>   Pathname of the error folder. </summary>
        internal static readonly string ErrorFolderName = "Errors";

        /// <summary>   Pathname of the event folder. </summary>
        internal static readonly string EventFolderName = "Events";

        /// <summary>   Pathname of the ınfo folder. </summary>
        internal static readonly string InfoFolderName = "Infos";

        /// <summary>   Pathname of the debug folder. </summary>
        internal static readonly string DebugFolderName = "Debugs";

        /// <summary>   Pathname of the static folder. </summary>
        internal static readonly string StaticFolderName = "Fldr_";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the pathname of the assembly directory. </summary>
        ///
        /// <value> The pathname of the assembly directory. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static string AssemblyDirectory
        {
            get
            {
                var directory = AppDomain.CurrentDomain.BaseDirectory;
                return directory;
            }
        }
    }
}