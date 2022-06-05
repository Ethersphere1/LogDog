using System.Runtime.InteropServices;

namespace LogDogBase
{
    internal class LoggerBase
    {
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int memcmp(byte[] b1, byte[] b2, long count);

        private static bool ByteArrayCompare(byte[] b1, byte[] b2)
        {
            // Validate buffers are the same length.
            // This also ensures that the count does not exceed the length of either buffer.
            return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
        } // ByteArrayCompare

        private static string currUserDocumentsAddr = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string tempDirPath = Path.Combine(currUserDocumentsAddr, "temp");
        private static string? tempFilePath;

        private string? logFileAddress;
        private string? outputLogFileName;
        private string? outputTempFileName;

        private int sleepBetweenLogCopy = 1000;

        public LoggerBase(string logFileAddress, string outputLogFileName, string outputTempFileName)
        {
            // check if temp folder in documents exists
            if (!Directory.Exists(tempDirPath))
            {
                Directory.CreateDirectory(tempDirPath);
            }

            this.logFileAddress = logFileAddress;
            this.outputLogFileName = outputLogFileName;
            this.outputTempFileName = outputTempFileName;

            tempFilePath = Path.Combine(tempDirPath, outputTempFileName);
            //Log.Information("Here 3");
        } // ctor

        public void LogFileTextFind()
        {

        }

        public void LogFileTask()
        {
            // copying log file with a delay to check if its updating
            byte[] fileNow = CopyLogFile();
            Thread.Sleep(sleepBetweenLogCopy);
            byte[] fileThen = CopyLogFile();

            if (!ByteArrayCompare(fileNow, fileThen))
            {
                File.Delete(tempFilePath);
            }
            else
            {
                if (!File.Exists(tempFilePath))
                {
                    CreateTempFile();
                }
                else
                {
                    //Log.Information($"{tempFilePath} already exists, program should not delete it");
                }
            }

        } // LogFileTask

        private byte[] CopyLogFile()
        {
            string logOutputDestinationFilePath = System.AppDomain.CurrentDomain.BaseDirectory + outputLogFileName;
            // copying log file to script directory
            if (File.Exists(logOutputDestinationFilePath))
            {
                File.Delete(logOutputDestinationFilePath); // if it already exists delete it to use the updated file
            }
            Thread.Sleep(150);
            try
            {
                File.Copy(logFileAddress, logOutputDestinationFilePath, true);
            }
            catch (IOException iox)
            {
                //Log.Information(iox.Message);
            }
            return File.ReadAllBytes(logOutputDestinationFilePath);
        } // copyLogFIle

        public void CreateTempFile()
        {
            if (!File.Exists(tempFilePath))
            {
                //Log.Information($"file {tempFilePath} doesn't exist");
                //Thread.Sleep(70);
                StreamWriter streamWriter = new StreamWriter(tempFilePath);
                Thread.Sleep(70);
                streamWriter.Close();
            }
        } // CreateTempFile
    } // class
} // namespace