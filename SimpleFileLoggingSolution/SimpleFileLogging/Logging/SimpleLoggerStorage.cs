using SimpleFileLogging.Enums;
using SimpleFileLogging.Interfaces;
using System.Collections.Concurrent;

namespace SimpleFileLogging.Logging
{
    /// <summary>
    /// SimpleLoggerStorage class. Stores ISimpleLogger instances.
    /// </summary>
    public static class SimpleLoggerStorage
    {
        private readonly static ConcurrentDictionary<byte, ISimpleLogger> loggerStorage = new ConcurrentDictionary<byte, ISimpleLogger>();

        /// <summary>
        /// Gets the simple logger instance for given Log Date Format.
        /// </summary>
        /// <param name="logDateFormat">The log date format.</param>
        /// <param name="enableMethodGrouping">if set to <c>true</c> [enable method grouping].</param>
        /// <returns>Returns ISimpleLogger instance.</returns>
        public static ISimpleLogger GetSimpleLogger(SimpleLogDateFormats logDateFormat, bool enableMethodGrouping = false)
        {
            ISimpleLogger logger = loggerStorage.GetOrAdd((byte)logDateFormat, (key) =>
            {
                return new SimpleFileLogger(logDateFormat);
            });

            logger.EnableMethodGrouping = enableMethodGrouping;
            return logger;
        }
    }
}
