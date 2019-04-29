namespace SimpleFileLogging
{
    /// <summary>
    /// Defines Log Type
    /// </summary>
    public enum SimpleLogType : byte
    {
        /// <summary>
        /// Logging for error
        /// </summary>
        Error = 10,

        /// <summary>
        /// Logging for info
        /// </summary>
        Info = 11,

        /// <summary>
        /// Logging for debug
        /// </summary>
        Debug = 12
    };
}