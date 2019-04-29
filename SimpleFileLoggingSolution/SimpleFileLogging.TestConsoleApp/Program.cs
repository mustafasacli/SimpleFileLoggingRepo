using SimpleFileLogging;
using SimpleFileLogging.Logging;
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
            MLogger.EnableMethodGrouping = false;
            MLogger.Info("Program started1.");
            MLogger.Debug("Program started1.");

            MLogger.Info("Program finished1.");
            MLogger.Debug("Program finished1.");
            try
            {
                throw new Exception("something something is going to dark side1.");
            }
            catch (Exception e)
            {
                MLogger.LogError(e);
            }

            MLogger.EnableMethodGrouping = true;
            MLogger.Info("Program started2.");
            MLogger.Debug("Program started2.");

            MLogger.Info("Program finished2.");
            MLogger.Debug("Program finished2.");

            try
            {
                throw new Exception("something something is going to dark side2.");
            }
            catch (Exception e)
            {
                MLogger.LogError(e);
            }

            Console.ReadKey();
        }
    }
}
