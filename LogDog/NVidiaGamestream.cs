using Serilog;

namespace LogDog
{
    internal class NVidiaGamestream
    {
        private static string programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        private static string nvidiaCopDirPath = Path.Combine(programDataPath, "NVIDIA Corporation");

        private static string nvStreamPath = Path.Combine(nvidiaCopDirPath, "NvStream");
        private static string nvLogFileName = "NvStreamerCurrent.log";
        private static string nvLogFilePath = Path.Combine(nvStreamPath, nvLogFileName);
        private static string outputTempFileName = "nVidiaGamestream.txt";

        private LoggerBase loggerBase;

        public NVidiaGamestream()
        {
            loggerBase = new LoggerBase(nvLogFilePath, nvLogFileName, outputTempFileName);

            try
            {
                while (true)
                {
                    //Log.Information("here 5");
                    if (File.Exists(nvLogFilePath))
                    {
                        Log.Information($"{nvLogFileName} found at {nvLogFilePath}");
                        loggerBase.LogFileTask();
                        //Log.Information("Here 6");

                    }
                    else
                    {
                        Log.Information($"{nvLogFileName} not found at {nvLogFilePath}");

                        string tempFilePath = Path.Combine(loggerBase.tempDirPath, outputTempFileName);
                        if (!File.Exists(tempFilePath))
                        {
                            loggerBase.CreateTempFile();
                            //Log.Information("Here 7");
                        }
                        //Log.Information("Here 8");

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