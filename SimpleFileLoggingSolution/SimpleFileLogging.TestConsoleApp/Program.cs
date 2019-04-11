using SimpleFileLogging;
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
            SimpleFileLogOptions.EnableMethodGrouping = false;
            SimpleFileLogger.Info("Program started1.");
            SimpleFileLogger.Debug("Program started1.");

            SimpleFileLogger.Info("Program finished1.");
            SimpleFileLogger.Debug("Program finished1.");
            try
            {
                throw new Exception("something something is going to dark side1.");
            }
            catch (Exception e)
            {
                SimpleFileLogger.LogError(e);
            }

            SimpleFileLogOptions.EnableMethodGrouping = true;
            SimpleFileLogger.Info("Program started2.");
            SimpleFileLogger.Debug("Program started2.");

            SimpleFileLogger.Info("Program finished2.");
            SimpleFileLogger.Debug("Program finished2.");

            try
            {
                throw new Exception("something something is going to dark side2.");
            }
            catch (Exception e)
            {
                SimpleFileLogger.LogError(e);
            }

            Console.ReadKey();
        }
    }
}
