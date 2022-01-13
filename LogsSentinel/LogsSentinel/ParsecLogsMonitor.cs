using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

namespace LogsSentinel
{


    internal class ParsecLogsMonitor
    {
        public ParsecLogsMonitor()
        {
            while (true)
            {
                Log.Information("this is from inside the parsecLog");
                Thread.Sleep(3000);

            }
        } // ctor
    } // class
} // namespace
