using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteCopy.Abstract;

namespace ConsoleAppSiteCopy
{
    public class Logger : ILogger
    {
        private readonly bool _isLogEnabled;

        public Logger(bool isLogEnabled)
        {
            _isLogEnabled = isLogEnabled;
        }

        public void Log(string message)
        {
            if (_isLogEnabled)
            {
                Console.WriteLine(message);
            }
        }
    }
}
