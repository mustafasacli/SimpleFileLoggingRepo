////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Logging\InternalFileLogger.cs
//
// summary:	Implements the internal file logger class
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace SimpleFileLogging
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A logging file operator. </summary>
    ///
    /// <remarks>   Msacli, 29.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    internal class LoggingFileOperator
    {
        /// <summary>   The lazy operation. </summary>
        private static Lazy<LoggingFileOperator> lazyOp =
            new Lazy<LoggingFileOperator>(() => new LoggingFileOperator());

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor that prevents a default instance of this class from being created.
        /// </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private LoggingFileOperator()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the ınstance. </summary>
        ///
        /// <value> The instance. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static LoggingFileOperator Instance
        { get { return lazyOp.Value; } }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes the given file. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ///
        /// <param name="filePath">     Full pathname of the file. </param>
        /// <param name="rows">         The rows. </param>
        /// <param name="writeLine">    (Optional) True to write line. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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