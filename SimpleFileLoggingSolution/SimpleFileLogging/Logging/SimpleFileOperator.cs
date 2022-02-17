////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	SimpleFileLogging\SimpleFileOperator.cs
//
// summary:	Implements the public file operator class
////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleFileLogging
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A logging file operator. </summary>
    ///
    /// <remarks>   Msacli, 29.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SimpleFileOperator
    {
        private static SimpleFileOperator instance = null;
        private static readonly object lockObject = new object();

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor that prevents a default instance of this class from being created.
        /// </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private SimpleFileOperator()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the ınstance. </summary>
        ///
        /// <value> The instance. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static SimpleFileOperator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new SimpleFileOperator();
                        }
                    }
                }

                return instance;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes the given file. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ///
        /// <param name="filePath">     Full pathname of the file. </param>
        /// <param name="rows">         The rows. </param>
        /// <param name="writeLine">    (Optional) True to write line. </param>
        /// <param name="autoFlushLineCount">Line count for file auto flush.</param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Write(string filePath, List<string> rows, bool writeLine = false, uint autoFlushLineCount = 5)
        {
            if (rows == null || rows.Count < 1)
                return;

            FileMode fileMode = File.Exists(filePath) ? FileMode.Append : FileMode.OpenOrCreate;

            using (StreamWriter writer = new StreamWriter(
                       new FileStream(filePath, fileMode))
            )
            {
                if (fileMode == FileMode.Append)
                    writer.WriteLine();

                autoFlushLineCount = autoFlushLineCount < 1 ? 1 : autoFlushLineCount;
                bool autoFlush = rows.Count < autoFlushLineCount;
                writer.AutoFlush = autoFlush;
                string empty = string.Empty;
                if (!writeLine)
                {
                    string text;
                    rows.ForEach(line =>
                    {
                        text = line ?? empty;
                        writer.Write(text);
                    });
                }
                else
                {
                    int lineCount = rows.Count;

                    for (int lineCounter = 0; lineCounter < lineCount; lineCounter++)
                    {
                        writer.Write(rows[lineCounter] ?? empty);
                        if (lineCounter < lineCount - 1)
                        { writer.WriteLine(empty); }
                    }
                }

                if (!autoFlush)
                    writer.Flush();
            }
        }

        /// <summary>
        /// Reads the lines.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Returns lines as string list.</returns>
        public static List<string> ReadLines(string fileName)
        {
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        /// <summary>
        /// Read Last the line.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="ignoreEmptyLine">Ignores empty line. if true ignores empty lines.</param>
        /// <returns>Read Last Line and returns as string.</returns>
        public static string ReadLastLine(string fileName, bool ignoreEmptyLine = false)
        {
            string lastLine = string.Empty;

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line) || !ignoreEmptyLine)
                    {
                        lastLine = string.Copy(line);
                    }
                }
            }

            return lastLine;
        }
    }
}