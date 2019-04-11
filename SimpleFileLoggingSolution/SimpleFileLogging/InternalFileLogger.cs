namespace SimpleFileLogging
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class LoggingFileOperator
    {
        private static Lazy<LoggingFileOperator> lazyOp =
            new Lazy<LoggingFileOperator>(() => new LoggingFileOperator());

        private LoggingFileOperator()
        {
        }

        public static LoggingFileOperator Instance
        { get { return lazyOp.Value; } }

        public void Write(string filePath, List<string> rows, bool writeLine = false)
        {
            try
            {
                FileMode fMode = File.Exists(filePath) ? FileMode.Append : FileMode.OpenOrCreate;

                using (StreamWriter writer = new StreamWriter(
                           new FileStream(filePath, fMode))
                { AutoFlush = true })
                {
                    if (!writeLine)
                    {
                        string str;
                        rows.ForEach(s =>
                        {
                            str = s ?? string.Empty;
                            if (str.EndsWith("\r\n") || str.EndsWith("\n"))
                            {
                                writer.Write(str);
                            }
                            else
                            {
                                writer.WriteLine(str);
                            }
                        });
                    }
                    else
                    {
                        rows.ForEach(s =>
                        {
                            writer.WriteLine(s ?? string.Empty);
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}