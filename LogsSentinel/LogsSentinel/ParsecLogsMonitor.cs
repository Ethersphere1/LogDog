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
        private List<string> parsecLogLastTwoLines; // = File.ReadLines(parsecLogFileAddress).Reverse().Take(2).Reverse().ToList();
        string tempPathParsec = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"temp\parsec.txt");
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
                // copying parsecLog file to script directory
                string parsecDestinationFile = System.AppDomain.CurrentDomain.BaseDirectory + "log.txt";
                if (File.Exists(parsecDestinationFile))
                {
                    File.Delete(parsecDestinationFile); // if it already exists delete it to use the updated file
                }
                try
                {
                    File.Copy(parsecLogFileAddress, parsecDestinationFile, true);
                }
                catch (IOException iox)
                {
                    Log.Information(iox.Message);
                }
                parsecLogLastTwoLines = File.ReadLines(parsecDestinationFile).Reverse().Take(2).Reverse().ToList();

                if (File.ReadLines(parsecDestinationFile).Count() < 2)
                {
                    // do nothing
                    { }
                }
                else
                {
                    //creating another list to compare logs after 10 seconds 
                    List<string> tempLastTwoLines = File.ReadLines(parsecDestinationFile).Reverse().Take(2).Reverse().ToList();

                    //create/delete 1.txt temp file
                    //string tempPath = @"C:\temp\1.txt";
                    if (tempLastTwoLines[0] == parsecLogLastTwoLines[0]
                        && tempLastTwoLines[1] == parsecLogLastTwoLines[1]
                        || !File.Exists(parsecDestinationFile)
                        )
                    {
                        if (!File.Exists(tempPathParsec))
                        {
                            Thread.Sleep(500);
                            //StreamWriter streamWriter = new StreamWriter(@"C:\temp\1.txt");
                            StreamWriter streamWriter = new StreamWriter(tempPathParsec);
                            Thread.Sleep(500);
                            parsecLogLastTwoLines = File.ReadLines(parsecDestinationFile).Reverse().Take(2).Reverse().ToList();
                            streamWriter.Close();
                        }
                    } // if
                    else
                    {
                        File.Delete(tempPathParsec);
                        parsecLogLastTwoLines = File.ReadLines(parsecDestinationFile).Reverse().Take(2).Reverse().ToList();
                    } // if-else
                }
                Thread.Sleep(second * 10);
            }
        } // LogFileTaskInit

    } // class
} // namespace
