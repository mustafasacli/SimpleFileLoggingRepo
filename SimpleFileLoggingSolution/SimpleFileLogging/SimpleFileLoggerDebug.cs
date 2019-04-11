﻿namespace SimpleFileLogging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    public partial class SimpleFileLogger
    {
        public static void Debug(params string[] messages)
        {
            if (messages == null || messages.Length < 1)
                return;

            try
            {
                DateTime dt = DateTime.Now;
                StackFrame frame = new StackFrame(1, true);
                MethodBase method = frame.GetMethod();
                int line = frame.GetFileLineNumber();
                int col = frame.GetFileColumnNumber();

                string assemblyName = method.Module.Assembly.FullName;
                string className = method.ReflectedType.Name;
                string assemblyFileName = frame.GetFileName();
                string methodName = method.Name;

                var debugFileName = string.Format(AppLoggingValues.DebugLogFileNameFormat, DateTime.Now.ToString(AppLoggingValues.LogFileDateFormat));

                var assemblyFolderName = method.Module.Assembly.GetName().Name.NormalizeString();
                var classFolderName = className.NormalizeString();
                var methodFolderName = methodName.NormalizeString();

                assemblyFolderName = string.IsNullOrWhiteSpace(assemblyFolderName) ? AppLoggingValues.StaticFolderName : assemblyFolderName;
                classFolderName = string.IsNullOrWhiteSpace(classFolderName) ? AppLoggingValues.StaticFolderName : classFolderName;
                methodFolderName = string.IsNullOrWhiteSpace(methodFolderName) ? AppLoggingValues.StaticFolderName : methodFolderName;

                string folderName = $"{AppLoggingValues.AssemblyDirectory}/{AppLoggingValues.InfoFolderName}/{assemblyFolderName}/{classFolderName}/{methodFolderName}";

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

                List<string> rows = new List<string>
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

        public static void Debug(Dictionary<string, string> dictionary)
        {
            if (dictionary == null || dictionary.Count < 1)
                return;

            try
            {
                DateTime dt = DateTime.Now;
                StackFrame frame = new StackFrame(1, true);
                MethodBase method = frame.GetMethod();
                int line = frame.GetFileLineNumber();
                int col = frame.GetFileColumnNumber();

                string assemblyName = method.Module.Assembly.FullName;
                string className = method.ReflectedType.Name;
                string assemblyFileName = frame.GetFileName();
                string methodName = method.Name;

                var debugFileName = string.Format(AppLoggingValues.DebugLogFileNameFormat, DateTime.Now.ToString(AppLoggingValues.LogFileDateFormat));

                var assemblyFolderName = method.Module.Assembly.GetName().Name.NormalizeString();
                var classFolderName = className.NormalizeString();
                var methodFolderName = methodName.NormalizeString();

                assemblyFolderName = string.IsNullOrWhiteSpace(assemblyFolderName) ? AppLoggingValues.StaticFolderName : assemblyFolderName;
                classFolderName = string.IsNullOrWhiteSpace(classFolderName) ? AppLoggingValues.StaticFolderName : classFolderName;
                methodFolderName = string.IsNullOrWhiteSpace(methodFolderName) ? AppLoggingValues.StaticFolderName : methodFolderName;

                string folderName = $"{AppLoggingValues.AssemblyDirectory}/{AppLoggingValues.InfoFolderName}/{assemblyFolderName}/{classFolderName}/{methodFolderName}";

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
    }
}