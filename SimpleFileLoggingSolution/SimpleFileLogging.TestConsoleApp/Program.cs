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
            SimpleFileLogger.Info("Program started.");
            SimpleFileLogger.Info("Program finished.");

            Console.ReadKey();
        }
    }
}
