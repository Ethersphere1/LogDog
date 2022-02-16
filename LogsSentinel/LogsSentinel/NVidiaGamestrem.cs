using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LogsSentinel
{
    internal class NVidiaGamestrem
    {
        private static string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private static string nvStreamerCurrentAddress = $@"{programData}\NVIDIA Corporation\NvStream\NvStreamerCurrent.log";
        private static string currUserDocumentsAddr = Environment.ExpandEnvironmentVariables(@"%HOMEPATH%");
        private static string tempPathMoon = $"{currUserDocumentsAddr}\\Documents\\temp\\nVidiaGamestream.txt";

        private List<string> moonLightAllLines;
        private List<string> moonlightLastTwoLines;
        private static int second = 1000;

        public NVidiaGamestrem()
        {
            // checking if NvStreamerCurrent.log exists 
            if (File.Exists(nvStreamerCurrentAddress))
            {
                Log.Information($"NvStreamerCurrent.log found at {nvStreamerCurrentAddress}");
                NVidiaGamestremLogFileTask();
            }
            else
            {
                Log.Information($"{nvStreamerCurrentAddress} not found");

                if (!File.Exists(tempPathMoon))
                {
                    Thread.Sleep(70);
                    StreamWriter streamWriter = new StreamWriter(tempPathMoon);
                    Thread.Sleep(70);
                    if (moonlightLastTwoLines.Count > 1)
                    {
                        moonlightLastTwoLines.Clear();
                    }
                    streamWriter.Close();
                }
            } // if-else
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

        public void NVidiaGamestremLogFileTask()
        {
            moonlightLastTwoLines = new List<string>();
            while (true)
            {
                moonLightAllLines = readAllLines(nvStreamerCurrentAddress);

                // moonlight log monitoring
                if (moonLightAllLines.Count < 2)
                {
                    // do nothing
                    { }
                }
                else
                {
                    // if temp directory doesn't exists in Documents create it
                    if (!Directory.Exists($"{currUserDocumentsAddr}\\Documents\\temp"))
                    {
                        Directory.CreateDirectory($"{currUserDocumentsAddr}\\Documents\\temp");
                    }

                    //create/delete nVidiaGamestream.txt temp file

                    if (moonlightLastTwoLines.Count > 1)
                    {
                        moonlightLastTwoLines.Clear();
                    }
                    moonlightLastTwoLines.Add(moonLightAllLines[moonLightAllLines.Count - 2]);
                    moonlightLastTwoLines.Add(moonLightAllLines[moonLightAllLines.Count - 1]);

                    if (!moonlightLastTwoLines[0].Contains("QoS", StringComparison.CurrentCultureIgnoreCase)
                        && !moonlightLastTwoLines[0].Contains("stream", StringComparison.CurrentCultureIgnoreCase)
                        && !moonlightLastTwoLines[1].Contains("QoS", StringComparison.CurrentCultureIgnoreCase)
                        && !moonlightLastTwoLines[1].Contains("stream", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!File.Exists(tempPathMoon))
                        {
                            Thread.Sleep(70);
                            StreamWriter streamWriter = new StreamWriter(tempPathMoon);
                            Thread.Sleep(70);
                            if (moonlightLastTwoLines.Count > 1)
                            {
                                moonlightLastTwoLines.Clear();
                            }
                            streamWriter.Close();
                        }
                    } // if
                    else
                    {
                        File.Delete(tempPathMoon);
                        moonlightLastTwoLines.Clear();
                    } // if-else

                } // else
                Thread.Sleep(second);

            } // while
        } // LogFileTaskInit

    } // class
} // namespace
