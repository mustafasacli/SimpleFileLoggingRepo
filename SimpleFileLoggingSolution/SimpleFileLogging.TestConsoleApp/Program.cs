using SimpleFileLogging.Enums;
using SimpleFileLogging.Logging;
using System;

namespace SimpleFileLogging.TestConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logger = SimpleFileLogger.Instance;

            logger.EnableMethodGrouping = false;
            logger.Info("Program started1.");
            logger.Debug("Program started1.");

            logger.Info("Program finished1.");
            logger.Debug("Program finished1.");
            try
            {
                throw new Exception("something something is going to dark side1.");
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }

            logger.LogDateFormatType = SimpleLogDateFormats.Minute;
            logger.EnableMethodGrouping = true;
            logger.Info("Program started2.");
            logger.Debug("Program started2.");

            logger.Info("Program finished2.");
            logger.Debug("Program finished2.");

            try
            {
                throw new Exception("something something is going to dark side2.");
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }

            SimpleFileLogger.Instance.Info("message1", "message2", "message3");

            var dayLogger = SimpleLoggerStorage.GetSimpleLogger(SimpleLogDateFormats.Day, enableMethodGrouping: true);
            dayLogger?.Info("message1-2", "message2-2", "message3-2");

            Console.ReadKey();
        }
    }
}