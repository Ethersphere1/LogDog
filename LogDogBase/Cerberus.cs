using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDogBase
{
    internal class Cerberus
    {
        private LoggerBase loggerBase;
        public Cerberus(string LogFilePath, string LogFileName, string OutputTempFileName,
            string WhereToCreateTxtFiles, string WhenToDeleteTempFile, string WhenToCreateTempFile)
        
        {
            int linesToTake = 500;
            string tempFilePath = Path.Combine(WhereToCreateTxtFiles, OutputTempFileName);
            loggerBase = new LoggerBase(LogFilePath, LogFileName, OutputTempFileName, tempFilePath);

            try
            {
                // checking if log.txt exists
                if (File.Exists(LogFilePath))
                {
                    //Log.Information($"{parsecLogFileName} found at {parsecLogPath}");

                    var lastLines = File.ReadAllLines(LogFilePath).Reverse().Take(linesToTake);
                    foreach (var line in lastLines)
                    {
                        if (line.Contains(WhenToDeleteTempFile))
                        {
                            //Console.WriteLine("Is connected");
                            File.Delete(tempFilePath);
                            break;
                        }
                        else if (line.Contains(WhenToCreateTempFile))
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
                    //Console.WriteLine("inside cerb");
                    //Console.WriteLine($"{LogFileName} not found at {LogFilePath}");
                    //Console.WriteLine(tempFilePath);
                    //Log.Information($"{LogFileName} not found at {LogFilePath}");
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
                //Log.Information("at this place");
            }
        }
    } // class
} // namesapce
