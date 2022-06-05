using Serilog;

namespace LogDogBase
{
    public class LDBase
    {
        public static string LogsDoglDir = AppDomain.CurrentDomain.BaseDirectory;
        public static string currUserDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string ethersphereinDocs = Path.Combine(currUserDocuments, "Ethersphere");
        public static string logDoginDocs = Path.Combine(ethersphereinDocs, "LogDog");
        public void hello()
        {
            //Console.WriteLine("From library");
            // if Ethersphere in documents doesn't exist
            if (!Directory.Exists(ethersphereinDocs))
            {
                Directory.CreateDirectory(ethersphereinDocs);
            }

            // if LogDog in Documents\Ethersphere doesn't exist
            if (!Directory.Exists(logDoginDocs))
            {
                Directory.CreateDirectory(logDoginDocs);
            }

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine(logDoginDocs, "logs.txt"),
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: 70000000)
                .CreateLogger();

            while (true)
            {
                //ParsecLogsMonitor parsecLogsMonitor = new ParsecLogsMonitor();

                SunshineLogsMonitor sunshineLogsMonitor = new SunshineLogsMonitor();
                Thread.Sleep(150);
                ParsecLogsMonitor parsecLogsMonitor = new ParsecLogsMonitor();

                //NVidiaGamestream nVidiaGamestream = new NVidiaGamestream();
            }
        }
        
    } // class
} // namespace