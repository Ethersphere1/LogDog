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
            string tempFilePath = Path.Combine(loggerBase.tempDirPath, outputTempFileName);
            int linesToTake = 500;

            try
            {
                //while (true)
                //{
                    if (File.Exists(sunshineLogPath))
                    {
                        Log.Information($"{sunshineLogFileName} found at {sunshineLogPath}");

                        var lastLines = File.ReadAllLines(sunshineLogPath).Reverse().Take(linesToTake);
                        foreach (var line in lastLines)
                        {
                            if (line.Contains("CLIENT CONNECTED"))
                            {
                                //Console.WriteLine("Is connected");
                                File.Delete(tempFilePath);
                                break;
                            }
                            else if (line.Contains("CLIENT DISCONNECTED"))
                            {
                                //Console.WriteLine("is disconnected");
                                if (!File.Exists(tempFilePath))
                                {
                                    loggerBase.CreateTempFile();
                                }
                                break;
                            }
                            //Console.WriteLine(line);
                        }

                        //loggerBase.LogFileTask();
                        //Log.Information("here 9");
                    }
                    else
                    {
                        Log.Information($"{sunshineLogFileName} not found at {sunshineLogPath}");

                        if (!File.Exists(tempFilePath))
                        {
                            loggerBase.CreateTempFile();
                            //Log.Information("here 10");
                        }
                        //Log.Information("here 11");
                    } // if-else
                    Thread.Sleep(150);
                //} // while
            }
            catch (Exception e)
            {
                Log.Information(e.Message);
            } // try-catch
        } // ctor

       
    } // class
} // namespace