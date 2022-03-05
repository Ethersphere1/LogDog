using Serilog;

namespace LogDog
{
    internal class SunshineLogsMonitor
    {
        private static string windowsDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

        private static string winTempDirPath = Path.Combine(windowsDir, "Temp");
        private static string sunshineLogFileName = "sunshine.log";
        private static string sunshineLogPath = Path.Combine(winTempDirPath, sunshineLogFileName);
        private static string outputTempFileName = "sunshine.txt";

        private LoggerBase loggerBase;

        public SunshineLogsMonitor()
        {
            loggerBase = new LoggerBase(sunshineLogPath, sunshineLogFileName, outputTempFileName);

            try
            {
                while (true)
                {
                    if (File.Exists(sunshineLogPath))
                    {
                        Log.Information($"{sunshineLogFileName} found at {sunshineLogPath}");
                        loggerBase.LogFileTask();
                    }
                    else
                    {
                        Log.Information($"{sunshineLogFileName} not found at {sunshineLogPath}");

                        string tempFilePath = Path.Combine(loggerBase.tempDirPath, outputTempFileName);
                        if (!File.Exists(tempFilePath))
                        {
                            loggerBase.CreateTempFile();
                        }
                    } // if-else
                } // while
            }
            catch (Exception e)
            {
                Log.Information(e.Message);
            } // try-cathc
        } // ctor
    } // class
} // namespace