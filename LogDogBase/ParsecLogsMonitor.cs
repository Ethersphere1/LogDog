using Serilog;

namespace LogDogBase
{
    internal class ParsecLogsMonitor
    {
        private static string appdataRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string parsecDirPath = Path.Combine(appdataRoamingPath, "Parsec");
        private static string parsecLogFileName = "log.txt";
        private static string parsecLogPath = Path.Combine(parsecDirPath, parsecLogFileName);
        private static string outputTempFileName = "parsec.txt";

        private LoggerBase loggerBase;

        public ParsecLogsMonitor()
        {
            loggerBase = new LoggerBase(parsecLogPath, parsecLogFileName, outputTempFileName);
            string tempFilePath = Path.Combine(loggerBase.tempDirPath, outputTempFileName);
            int linesToTake = 500;

            try
            {
                // checking if log.txt exists
                if (File.Exists(parsecLogPath))
                {
                    //Log.Information($"{parsecLogFileName} found at {parsecLogPath}");

                    var lastLines = File.ReadAllLines(parsecLogPath).Reverse().Take(linesToTake);
                    foreach (var line in lastLines)
                    {
                        if (line.Contains(" connected."))
                        {
                            //Console.WriteLine("Is connected");
                            File.Delete(tempFilePath);
                            break;
                        }
                        else if (line.Contains(" disconnected."))
                        {
                            //Console.WriteLine("is disconnected");
                            if (!File.Exists(tempFilePath))
                            {
                                loggerBase.CreateTempFile();
                            }
                            break;
                        }
                    } // foreach
                }
                else
                {
                    Log.Information($"{parsecLogFileName} not found at {parsecLogPath}");


                    if (!File.Exists(tempFilePath))
                    {
                        loggerBase.CreateTempFile();
                    }
                } // if-else
                Thread.Sleep(50);
                //} // while
            }
            catch (Exception e)
            {
                Log.Information(e.Message);
            } // try-catch
        } // ctor
    } // class
} // namespace