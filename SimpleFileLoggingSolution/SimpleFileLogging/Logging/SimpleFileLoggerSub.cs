////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Logging\SimpleFileLoggerSub.cs
//
// summary:	Implements the simple file logger sub class
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace SimpleFileLogging
{
    using SimpleFileLogging.Enums;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Simple Logger for file logging. </summary>
    ///
    /// <remarks>   Msacli, 29.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class SimpleFileLogger
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Logs. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ///
        /// <param name="logType">      Log Type. </param>
        /// <param name="dictionary">   The dictionary. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Log(SimpleLogType logType, Dictionary<string, string> dictionary)
        {
            if (dictionary == null || dictionary.Count < 1)
                return;

            try
            {
                DateTime dt = DateTime.Now;
                StackFrame frame = new StackFrame(2, true);
                MethodBase method = frame.GetMethod();
                int line = frame.GetFileLineNumber();
                int col = frame.GetFileColumnNumber();

                string assemblyName = method.Module.Assembly.FullName;
                string className = method.ReflectedType.Name;
                string assemblyFileName = frame.GetFileName();
                string methodName = method.Name;

                var debugFileName = GetLogFileName(logType);
                var folderName = BuildLogFolderName(logType, method.Module.Assembly.GetName().Name, className, methodName);

                try
                {
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }
                }
                catch (Exception)
                {
                }

                string fileName = $"{folderName}/{debugFileName}";

                var rows = new List<string>
                    {
                        $"Time : {dt.ToString(AppLoggingValues.GeneralDateFormat)}",
                        $"Assembly : {assemblyName}",
                        $"Assembly File Name : {assemblyFileName}",
                        $"Class : {className}",
                        $"Method Name : {methodName}",
                        $"Line : {line}",
                        $"Column : {col}"
                    };

                var additions = DictionaryToList(dictionary);
                rows.AddRange(additions);
                rows.Add(AppLoggingValues.Lines);

                LoggingFileOperator.Instance.Write(fileName, rows);
            }
            catch (Exception ee)
            {
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Logs. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ///
        /// <param name="logType">  Log Type. </param>
        /// <param name="messages"> A variable-length parameters list containing messages. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Log(SimpleLogType logType, params string[] messages)
        {
            if (messages == null || messages.Length < 1)
                return;

            try
            {
                DateTime dt = DateTime.Now;
                StackFrame frame = new StackFrame(2, true);
                MethodBase method = frame.GetMethod();
                int line = frame.GetFileLineNumber();
                int col = frame.GetFileColumnNumber();

                var assemblyName = method.Module.Assembly.FullName;
                var className = method.ReflectedType.Name;
                var assemblyFileName = frame.GetFileName();
                var methodName = method.Name;

                var errorFileName = GetLogFileName(logType);
                var folderName = BuildLogFolderName(logType, method.Module.Assembly.GetName().Name, className, methodName);

                try
                {
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }
                }
                catch (Exception)
                {
                }

                string fileName = $"{folderName}/{errorFileName}";

                var rows = new List<string>
                    {
                        $"Time : {dt.ToString(AppLoggingValues.GeneralDateFormat)}",
                        $"Assembly : {assemblyName}",
                        $"Assembly File Name : {assemblyFileName}",
                        $"Class : {className}",
                        $"Method Name : {methodName}",
                        $"Line : {line}",
                        $"Column : {col}"
                    };

                rows.AddRange(messages);
                rows.Add(AppLoggingValues.Lines);

                LoggingFileOperator.Instance.Write(fileName, rows);
            }
            catch (Exception ee)
            {
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets Log File Name. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ///
        /// <param name="logType">  Log Type. </param>
        ///
        /// <returns>   returns Log File Name. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected string GetLogFileName(SimpleLogType logType)
        {
            var logFileName = string.Empty;
            switch (logType)
            {
                case SimpleLogType.Error:
                    logFileName = AppLoggingValues.ErrorLogFileNameFormat;
                    break;

                case SimpleLogType.Info:
                    logFileName = AppLoggingValues.InfoLogFileNameFormat;
                    break;

                case SimpleLogType.Debug:
                    logFileName = AppLoggingValues.DebugLogFileNameFormat;
                    break;

                default:
                    break;
            }

            var dateString = string.Empty;
            if (this.SimpleLogDateFormatType == SimpleLogDateFormatTypes.None)
                logFileName = logFileName.Replace('-', '\0');
            else
                dateString = DateTime.Now.ToString(GetFileDateFormat(this.SimpleLogDateFormatType));
            //if (this.SimpleLogDateFormatType != SimpleLogDateFormatTypes.None)
            //dateString = DateTime.Now.ToString(GetFileDateFormat(this.SimpleLogDateFormatType)); //AppLoggingValues.LogFileDateFormat);

            logFileName = string.Format(logFileName, dateString);

            return logFileName;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets file date format. </summary>
        ///
        /// <remarks>   Msacli, 17.05.2019. </remarks>
        ///
        /// <param name="simpleLogDateFormatType">  Type of the simple log date format. </param>
        ///
        /// <returns>   The file date format. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected string GetFileDateFormat(SimpleLogDateFormatTypes simpleLogDateFormatType)
        {
            var s = string.Empty;

            switch (simpleLogDateFormatType)
            {
                case SimpleLogDateFormatTypes.None:
                    break;

                case SimpleLogDateFormatTypes.Second:
                    s = "yyyy-MM-dd-HH-mm-ss";
                    break;

                case SimpleLogDateFormatTypes.Minute:
                    s = "yyyy-MM-dd-HH-mm";
                    break;

                case SimpleLogDateFormatTypes.Hour:
                    s = "yyyy-MM-dd-HH";
                    break;

                case SimpleLogDateFormatTypes.Day:
                    s = "yyyy-MM-dd";
                    break;

                case SimpleLogDateFormatTypes.Month:
                    s = "yyyy-MM";
                    break;

                case SimpleLogDateFormatTypes.Year:
                    s = "yyyy";
                    break;

                default:
                    break;
            }

            return s;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Builds log folder name. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ///
        /// <param name="logType">      Log Type. </param>
        /// <param name="assemblyName"> Name of the assembly. </param>
        /// <param name="className">    Name of the class. </param>
        /// <param name="methodName">   Name of the method. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected string BuildLogFolderName(SimpleLogType logType, string assemblyName, string className, string methodName)
        {
            var assemblyFolderName = assemblyName.NormalizeString();
            var classFolderName = className.NormalizeString();
            var methodFolderName = methodName.NormalizeString();

            assemblyFolderName = string.IsNullOrWhiteSpace(assemblyFolderName) ? AppLoggingValues.StaticFolderName : assemblyFolderName;
            classFolderName = string.IsNullOrWhiteSpace(classFolderName) ? AppLoggingValues.StaticFolderName : classFolderName;
            methodFolderName = string.IsNullOrWhiteSpace(methodFolderName) ? AppLoggingValues.StaticFolderName : methodFolderName;

            var folderName = string.Empty;
            var logTypefolderName = GetLogTypeFolderName(logType);

            if (this.EnableMethodGrouping)
                folderName = $"{AppLoggingValues.AssemblyDirectory}/{assemblyFolderName}/{classFolderName}/{methodFolderName}/{logTypefolderName}";
            else
                folderName = $"{AppLoggingValues.AssemblyDirectory}/{logTypefolderName}/{assemblyFolderName}/{classFolderName}/{methodFolderName}";

            return folderName;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets log type folder name. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ///
        /// <param name="logType">  Log Type. </param>
        ///
        /// <returns>   The log type folder name. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected string GetLogTypeFolderName(SimpleLogType logType)
        {
            var s = string.Empty;

            switch (logType)
            {
                case SimpleLogType.Error:
                    s = AppLoggingValues.ErrorFolderName;
                    break;

                case SimpleLogType.Info:
                    s = AppLoggingValues.InfoFolderName;
                    break;

                case SimpleLogType.Debug:
                    s = AppLoggingValues.DebugFolderName;
                    break;

                default:
                    break;
            }

            return s;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Dictionary to list. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ///
        /// <param name="dictionary">   The dictionary. </param>
        ///
        /// <returns>   A List&lt;string&gt; </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected List<string> DictionaryToList(Dictionary<string, string> dictionary)
        {
            var list = new List<string> { };

            if (dictionary == null)
                return list;

            dictionary
                .Keys
                .ToList()
                .ForEach(q =>
                {
                    list.Add($"{q} : {dictionary[q]}");
                });

            return list;
        }
    }
}