namespace SimpleFileLogging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    public partial class SimpleFileLogger
    {
        public static void LogError(Exception e)
        {
            if (e == null)
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

                var errorFileName = string.Format(AppLoggingValues.ErrorLogFileNameFormat, DateTime.Now.ToString(AppLoggingValues.ErrorFileDateFormat));

                var assemblyFolderName = method.Module.Assembly.GetName().Name.NormalizeString();
                var classFolderName = className.NormalizeString();
                var methodFolderName = methodName.NormalizeString();

                assemblyFolderName = string.IsNullOrWhiteSpace(assemblyFolderName) ? AppLoggingValues.StaticFolderName : assemblyFolderName;
                classFolderName = string.IsNullOrWhiteSpace(classFolderName) ? AppLoggingValues.StaticFolderName : classFolderName;
                methodFolderName = string.IsNullOrWhiteSpace(methodFolderName) ? AppLoggingValues.StaticFolderName : methodFolderName;

                string folderName = $"{AppLoggingValues.AssemblyDirectory}/{AppLoggingValues.ErrorFolderName}/{assemblyFolderName}/{classFolderName}/{methodFolderName}";

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
                        $"Column : {col}",
                        $"Message : {e.Message}",
                        $"Stack Trace : {e.StackTrace}",
                        $"Exception Data : {e.ToString()}",
                        AppLoggingValues.Lines
                    };

                LoggingFileOperator.Instance.Write(fileName, rows);
            }
            catch (Exception ee)
            {
            }
        }

        public static void LogError(params string[] messages)
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

                var errorFileName = string.Format(AppLoggingValues.ErrorLogFileNameFormat, DateTime.Now.ToString(AppLoggingValues.ErrorFileDateFormat));

                var assemblyFolderName = method.Module.Assembly.GetName().Name.NormalizeString();
                var classFolderName = className.NormalizeString();
                var methodFolderName = methodName.NormalizeString();

                assemblyFolderName = string.IsNullOrWhiteSpace(assemblyFolderName) ? AppLoggingValues.StaticFolderName : assemblyFolderName;
                classFolderName = string.IsNullOrWhiteSpace(classFolderName) ? AppLoggingValues.StaticFolderName : classFolderName;
                methodFolderName = string.IsNullOrWhiteSpace(methodFolderName) ? AppLoggingValues.StaticFolderName : methodFolderName;

                string folderName = $"{AppLoggingValues.AssemblyDirectory}/{AppLoggingValues.ErrorFolderName}/{assemblyFolderName}/{classFolderName}/{methodFolderName}";

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
    }
}