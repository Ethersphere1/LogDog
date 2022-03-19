using Serilog;

namespace LogDog
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Log.Information("Here 1");
            //SunshineTask.Start();
            //Thread.Sleep(150);
            //ParsecTask.Start();
            //Thread.Sleep(150);
            //MoonlightTast.Start();
            //Log.Information("Here 2");

            while (true)
            {
                ParsecLogsMonitor parsecLogsMonitor = new ParsecLogsMonitor();

                SunshineLogsMonitor sunshineLogsMonitor = new SunshineLogsMonitor();
            }

            //ParsecLogsMonitor parsecLogsMonitor = new ParsecLogsMonitor();
            //await parsecLogsMonitor.ParsecLogsMonitorAsync();

            //SunshineLogsMonitor sunshineLogsMonitor = new SunshineLogsMonitor();
            //await sunshineLogsMonitor.SunshineLogsMonitorAsync();
        }

        //public async Task ExecuteAsync()
        //{
        //    ParsecLogsMonitor parsecLogsMonitor = new ParsecLogsMonitor();
        //    parsecLogsMonitor.ParsecLogsMonitorAsync();
        //}

        //private Task ParsecTask = new Task(() =>
        //  {
        //      //Log.Information("here 15");
        //      ParsecLogsMonitor parsecLogsMonitor = new ParsecLogsMonitor();
        //  });

        //private Task MoonlightTast = new Task(() =>
        //  {
        //      //Log.Information("here 16");
        //      NVidiaGamestream moonlightLogsMonitor = new NVidiaGamestream();
        //  });

        //private Task SunshineTask = new Task(() =>
        //  {
        //      //Log.Information("here 17");
        //      SunshineLogsMonitor sunshineLogsMonitor = new SunshineLogsMonitor();
        //  });
    } // worker
} // namespace