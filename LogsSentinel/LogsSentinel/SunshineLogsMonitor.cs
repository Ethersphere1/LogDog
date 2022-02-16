using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LogsSentinel
{
    internal class SunshineLogsMonitor
    {
        private static string windowsDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        private static string sunshineAddress = $@"{windowsDir}\Temp\sunshine.log";
        private static string currUserDocumentsAddr = Environment.ExpandEnvironmentVariables(@"%HOMEPATH%");
        private static string tempPathSun = $"{currUserDocumentsAddr}\\Documents\\temp\\sunshine.txt";

        private List<string> sunshineAllLines;
        private List<string> sunshineLastTwoLines;

        public SunshineLogsMonitor()
        {
            // checking if sunshine.log exists 
            if (File.Exists(sunshineAddress))
            {
                Log.Information($"sunshine.log found at {sunshineAddress}");
                SunshineLogFileTask();
            }
            else
            {
                Log.Information($"sunshine.log not found at {sunshineAddress}");
            }
            
        } // ctor

        private static List<string> readAllLines(string path)
        {
            List<string> allLines = new List<string>();
            // read all lines,
            using (StreamReader file = new StreamReader(File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)))
            {
                int counter = 0;
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    allLines.Add(ln);
                    counter++;
                }
                file.Close();
            }
            return allLines;
        } // readAllLines

        public void SunshineLogFileTask()
        {
            sunshineLastTwoLines = new List<string>();
            while (true)
            {
                // copying sunshine file to script directory
                string sunDestinationFile = System.AppDomain.CurrentDomain.BaseDirectory + "sunshine.log";
                if (File.Exists(sunDestinationFile))
                {
                    File.Delete(sunDestinationFile); // if it already exists delete it to use the updated file
                }
                try
                {
                    File.Copy(sunshineAddress, sunDestinationFile, true);
                }
                catch (IOException iox)
                {
                    Log.Information(iox.Message);
                }
                sunshineAllLines = readAllLines(sunDestinationFile);

                // moonlight log monitoring
                if (sunshineAllLines.Count < 2)
                {
                    // do nothing
                    Log.Information($"sunshine.log found at {sunshineAddress} is empty");
                }
                else
                {
                    // if temp directory doesn't exists in Documents create it
                    if (!Directory.Exists($"{currUserDocumentsAddr}\\Documents\\temp"))
                    {
                        Directory.CreateDirectory($"{currUserDocumentsAddr}\\Documents\\temp");
                    }

                    //create/delete sunshine.txt temp file

                    if (sunshineLastTwoLines.Count > 1)
                    {
                        sunshineLastTwoLines.Clear();
                    }
                    sunshineLastTwoLines.Add(sunshineAllLines[sunshineAllLines.Count - 2]);
                    sunshineLastTwoLines.Add(sunshineAllLines[sunshineAllLines.Count - 1]);

                    //sunshineLastTwoLines = File.ReadLines(sunshineAddress).Reverse().Take(2).Reverse().ToList();

                    if (!sunshineLastTwoLines[0].Contains("GameStream Client", StringComparison.CurrentCultureIgnoreCase)
                        //&& !sunshineLastTwoLines[0].Contains("Client", StringComparison.CurrentCultureIgnoreCase)
                        && !sunshineLastTwoLines[1].Contains("GameStream Client", StringComparison.CurrentCultureIgnoreCase)
                        //&& !sunshineLastTwoLines[1].Contains("Client", StringComparison.CurrentCultureIgnoreCase)
                        )
                    {
                        if (!File.Exists(tempPathSun))
                        {
                            Thread.Sleep(70);
                            StreamWriter streamWriter = new StreamWriter(tempPathSun);
                            Thread.Sleep(70);
                            if (sunshineLastTwoLines.Count > 1)
                            {
                                sunshineLastTwoLines.Clear();
                            }
                            streamWriter.Close();
                        }
                    } // if
                    else
                    {
                        File.Delete(tempPathSun);
                        sunshineLastTwoLines.Clear();
                    } // if-else

                } // else
                //File.Delete(destinationFile);
                Thread.Sleep(300);
                

            } // while
        } // LogFileTaskInit

    } // class
} // namespace
