using Serilog;

namespace LogDog
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

            try
            {
                while (true)
                {
                    // checking if sunshine.log exists
                    if (File.Exists(parsecLogPath))
                    {
                        Log.Information($"{parsecLogFileName} found at {parsecLogPath}");
                        loggerBase.LogFileTask();
                    }
                    else
                    {
                        Log.Information($"{parsecLogFileName} not found at {parsecLogPath}");

                        string tempFilePath = Path.Combine(loggerBase.tempDirPath, outputTempFileName);
                        if (!File.Exists(tempFilePath))
                        {
                            loggerBase.CreateTempFile();
                        }
                    } // if-else
                    Thread.Sleep(150);
                } // while
            }
            catch (Exception e)
            {
                Log.Information(e.Message);
            } // try-catch
        } // ctor
    } // class
} // namespace