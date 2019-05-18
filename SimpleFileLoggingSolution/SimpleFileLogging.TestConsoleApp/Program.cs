using SimpleFileLogging;
using SimpleFileLogging.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileLogging.TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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

            Console.ReadKey();
        }
    }
}
