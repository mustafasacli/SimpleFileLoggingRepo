namespace SimpleFileLogging
{
    using System;

    public class AppLoggingValues
    {
        internal static readonly string LogFileNameFormat = "event-{0}.log";

        internal static readonly string ErrorLogFileNameFormat = "error-{0}.log";
        internal static readonly string InfoLogFileNameFormat = "info-{0}.log";
        internal static readonly string DebugLogFileNameFormat = "debug-{0}.log";

        internal static readonly string LogFileDateFormat = "yyyy-MM-dd-HH-mm-ss";

        internal static readonly string ErrorFileDateFormat = "yyyy-MM-dd-HH-mm-ss";

        internal static readonly string GeneralDateFormat = "yyyy-MM-dd, HH:mm:ss ffffff";

        internal static readonly string Lines = "----------------------------------";

        internal static readonly string ErrorFolderName = "Errors";
        internal static readonly string EventFolderName = "Events";
        internal static readonly string InfoFolderName = "Infos";
        internal static readonly string DebugFolderName = "Debugs";

        internal static readonly string StaticFolderName = "Fldr_";

        internal static string AssemblyDirectory
        {
            get
            {
                var dir = AppDomain.CurrentDomain.BaseDirectory;
                return dir;
            }
        }
    }
}