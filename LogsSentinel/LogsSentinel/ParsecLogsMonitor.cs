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
        private static string appdataRoamingAddress = Environment.ExpandEnvironmentVariables(@"%AppData%");
        private static string parsecLogFileAddress = $@"{appdataRoamingAddress}\Parsec\log.txt";
        private List<string> parsecLogLastTwoLines = File.ReadLines(parsecLogFileAddress).Reverse().Take(2).Reverse().ToList();
        private static int second = 1000;

        public ParsecLogsMonitor()
        {
            // checking if log.txt exists 
            if (File.Exists(parsecLogFileAddress))
            {
                Log.Information($"log.txt found at {parsecLogFileAddress}");
                ParsecLogFileTask();
            }
            else
            {
                Log.Information($"log.txt not found at {parsecLogFileAddress}");
            }
        } // ctor

        public void ParsecLogFileTask()
        {
            while (true)
            {

                if (File.ReadLines(parsecLogFileAddress).Count() < 2)
                {
                    // do nothing
                    { }
                }
                else
                {
                    //creating another list to compare logs after 10 seconds 
                    List<string> tempLastTwoLines = File.ReadLines(parsecLogFileAddress).Reverse().Take(2).Reverse().ToList();

                    //create/delete 1.txt temp file
                    //string tempPath = @"C:\temp\1.txt";
                    string tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"temp\parsec.txt");
                    if (tempLastTwoLines[0] == parsecLogLastTwoLines[0]
                        && tempLastTwoLines[1] == parsecLogLastTwoLines[1]
                        || !File.Exists(parsecLogFileAddress)
                        )
                    {
                        if (!File.Exists(tempPath))
                        {
                            Thread.Sleep(500);
                            //StreamWriter streamWriter = new StreamWriter(@"C:\temp\1.txt");
                            StreamWriter streamWriter = new StreamWriter(tempPath);
                            Thread.Sleep(500);
                            parsecLogLastTwoLines = File.ReadLines(parsecLogFileAddress).Reverse().Take(2).Reverse().ToList();
                            streamWriter.Close();
                        }
                    } // if
                    else
                    {
                        File.Delete(tempPath);
                        parsecLogLastTwoLines = File.ReadLines(parsecLogFileAddress).Reverse().Take(2).Reverse().ToList();
                    } // if-else
                }
                Thread.Sleep(second * 10);
            }
        } // LogFileTaskInit

    } // class
} // namespace
