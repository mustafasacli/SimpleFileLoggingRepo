namespace SimpleFileLogging
{
    /// <summary>
    /// Defining Logging Options
    /// </summary>
    public static class SimpleFileLogOptions
    {
        /// <summary>
        /// Enables Method Grouping
        /// if it is true AssemblyFolder\ClassName\MethodName\Error-Info-Debug\logfile.
        /// else AssemblyFolder\Error-Info-Debug\ClassName\MethodName\logfile.
        /// </summary>
        public static bool EnableMethodGrouping
        { get; set; } = false;
    }
}