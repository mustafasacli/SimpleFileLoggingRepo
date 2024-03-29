﻿////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Logging\SimpleFileLoggerSub.cs
//
// summary:	Implements the simple file logger sub class
////////////////////////////////////////////////////////////////////////////////////////////////////
using SimpleFileLogging.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SimpleFileLogging
{
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
        protected void Log(SimpleLogType logType,
            Dictionary<string, string> dictionary)
        {
            try
            {
                if (dictionary == null || dictionary.Count < 1)
                    return;

                DateTime dt = DateTime.Now;
                StackFrame frame = new StackFrame(2, true);
                MethodBase method = frame.GetMethod();

                string assemblyName = method.Module.Assembly.FullName;
                string className = method.ReflectedType.Name;
                string assemblyFileName = frame.GetFileName();
                string methodName = method.Name;
                int line = frame.GetFileLineNumber();
                int col = frame.GetFileColumnNumber();

                var debugFileName = GetLogFileName(logType);
                var folderName = BuildLogFolderName(logType, method.Module.Assembly.GetName().Name, className, methodName);

                try
                {
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }
                }
                catch (Exception e2)
                { WriteException(e2); }

                string fileName = $"{folderName}/{debugFileName}";

                var rows = new List<string>
                {
                        $"Time : {dt.ToString(AppLoggingValues.GeneralDateFormat)}"
                };

                rows.Add($"Assembly : {assemblyName}");
                rows.Add($"Assembly File Name : {assemblyFileName}");
                rows.Add($"Class : {className}");
                rows.Add($"Method Name : {methodName}");
                rows.Add($"Line : {line}");
                rows.Add($"Column : {col}");

                var additions = DictionaryToList(dictionary);
                rows.AddRange(additions);
                rows.Add(AppLoggingValues.Lines);

                SimpleFileOperator.Instance.Write(fileName, rows, writeLine: true);
            }
            catch (Exception ee)
            { WriteException(ee); }
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
            try
            {
                if (messages == null || messages.Length < 1)
                    return;

                var checkMessages = messages
                    .Any(q => !string.IsNullOrWhiteSpace(q));

                if (!checkMessages)
                    return;

                DateTime dt = DateTime.Now;
                StackFrame frame = new StackFrame(2, true);
                MethodBase method = frame.GetMethod();

                var assemblyName = method.Module.Assembly.FullName;
                var className = method.ReflectedType.Name;
                var assemblyFileName = frame.GetFileName();
                var methodName = method.Name;
                int line = frame.GetFileLineNumber();
                int col = frame.GetFileColumnNumber();

                var errorFileName = GetLogFileName(logType);
                var folderName = BuildLogFolderName(logType,
                    method.Module.Assembly.GetName().Name, className, methodName);

                try
                {
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }
                }
                catch (Exception e2)
                { WriteException(e2); }

                string fileName = $"{folderName}/{errorFileName}";

                var rows = new List<string>
                {
                    $"Time : {dt.ToString(AppLoggingValues.GeneralDateFormat)}"
                };

                rows.Add($"Assembly : {assemblyName}");
                rows.Add($"Assembly File Name : {assemblyFileName}");
                rows.Add($"Class : {className}");
                rows.Add($"Method Name : {methodName}");
                rows.Add($"Line : {line}");
                rows.Add($"Column : {col}");

                rows.AddRange(messages);
                rows.Add(AppLoggingValues.Lines);

                SimpleFileOperator.Instance.Write(fileName, rows, writeLine: true);
            }
            catch (Exception ee)
            { WriteException(ee); }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Logs. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        /// 
        /// <param name="logOptions"></param>
        /// <param name="logType"></param>
        /// <param name="messages"> A variable-length parameters list containing messages. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void LogV2(SimpleFileLogOptions logOptions, SimpleLogType logType, params string[] messages)
        {
            try
            {
                if (messages == null || messages.Length < 1)
                    return;

                var checkMessages = messages
                    .Any(q => !string.IsNullOrWhiteSpace(q));

                if (!checkMessages)
                    return;

                DateTime dt = DateTime.Now;
                StackFrame frame = new StackFrame(2, true);
                MethodBase method = frame.GetMethod();

                var assemblyName = method.Module.Assembly.FullName;
                var className = method.ReflectedType.Name;
                var assemblyFileName = frame.GetFileName();
                var methodName = method.Name;
                int line = frame.GetFileLineNumber();
                int col = frame.GetFileColumnNumber();

                var errorFileName = GetLogFileName(logType);
                var folderName = BuildLogFolderName(logType,
                    method.Module.Assembly.GetName().Name, className, methodName);

                try
                {
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }
                }
                catch (Exception e2)
                { WriteException(e2); }

                string fileName = $"{folderName}/{errorFileName}";

                var rows = new List<string>
                {
                    $"Time : {dt.ToString(AppLoggingValues.GeneralDateFormat)}"
                };

                if (!logOptions.ExcludeAssemblyInfo)
                {
                    rows.Add($"Assembly : {assemblyName}");
                    rows.Add($"Assembly File Name : {assemblyFileName}");
                    rows.Add($"Class : {className}");
                    rows.Add($"Method Name : {methodName}");
                    rows.Add($"Line : {line}");
                    rows.Add($"Column : {col}");
                }

                rows.AddRange(messages);
                rows.Add(AppLoggingValues.Lines);

                SimpleFileOperator.Instance.Write(fileName, rows, writeLine: true);
            }
            catch (Exception ee)
            { WriteException(ee); }
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
            if (this.LogDateFormatType == SimpleLogDateFormats.None)
                logFileName = logFileName.Replace("-", string.Empty);
            else
                dateString = DateTime.Now.ToString(GetFileDateFormat(this.LogDateFormatType));

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
        protected string GetFileDateFormat(SimpleLogDateFormats simpleLogDateFormatType)
        {
            var fileDateFormat = string.Empty;

            switch (simpleLogDateFormatType)
            {
                case SimpleLogDateFormats.None:
                    break;

                case SimpleLogDateFormats.Second:
                    fileDateFormat = "yyyy-MM-dd-HH-mm-ss";
                    break;

                case SimpleLogDateFormats.Minute:
                    fileDateFormat = "yyyy-MM-dd-HH-mm";
                    break;

                case SimpleLogDateFormats.Hour:
                    fileDateFormat = "yyyy-MM-dd-HH";
                    break;

                case SimpleLogDateFormats.Day:
                    fileDateFormat = "yyyy-MM-dd";
                    break;

                case SimpleLogDateFormats.Month:
                    fileDateFormat = "yyyy-MM";
                    break;

                case SimpleLogDateFormats.Year:
                    fileDateFormat = "yyyy";
                    break;

                default:
                    break;
            }

            return fileDateFormat;
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
            var logTypeFolderName = string.Empty;

            switch (logType)
            {
                case SimpleLogType.Error:
                    logTypeFolderName = AppLoggingValues.ErrorFolderName;
                    break;

                case SimpleLogType.Info:
                    logTypeFolderName = AppLoggingValues.InfoFolderName;
                    break;

                case SimpleLogType.Debug:
                    logTypeFolderName = AppLoggingValues.DebugFolderName;
                    break;

                default:
                    break;
            }

            return logTypeFolderName;
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