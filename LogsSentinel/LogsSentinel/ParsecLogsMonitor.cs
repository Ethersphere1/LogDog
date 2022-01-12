using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsSentinel
{
    internal class ParsecLogsMonitor
    {
        public ParsecLogsMonitor()
        {
            while (true)
            {
                System.Diagnostics.Debug.WriteLine("Inside Parsec ******* ");
                Thread.Sleep(3000);
            }
        }
    }
}
