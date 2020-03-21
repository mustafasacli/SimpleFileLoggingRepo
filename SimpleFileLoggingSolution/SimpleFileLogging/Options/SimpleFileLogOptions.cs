namespace SimpleFileLogging
{
    /// <summary>
    /// Defining Logging Options
    /// </summary>
    public class SimpleFileLogOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether EnableMethodGrouping
        /// Enables Method Grouping
        /// if it is true AssemblyFolder\ClassName\MethodName\Error-Info-Debug\logfile.
        /// else AssemblyFolder\Error-Info-Debug\ClassName\MethodName\logfile.
        /// </summary>
        public bool EnableMethodGrouping
        { get; set; } = false;

        /// <summary>
        /// Gets or sets the ExcludeAssemblyInfo
        /// if true, assembly info will not be logged else will be logged.
        /// </summary>
        public bool ExcludeAssemblyInfo
        { get; set; } = false;
    }
}
