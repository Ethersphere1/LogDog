namespace LogsSentinel
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
            ParsecTask.Start();
            //var parsecObject = new ParsecLogsMonitor();
            //var parsecThread = new Thread(parsecObject.ParsecLogFileTask);
            //parsecThread.Start();

            MoonlightTast.Start();
            //var nvstreamObject = new NVidiaGamestrem();
            //var nvstreamThread = new Thread(nvstreamObject.NVidiaGamestremLogFileTask);
            //nvstreamThread.Start();

            SunshineTask.Start();
            //var sunshineObject = new SunshineLogsMonitor();
            //var sunshineThread = new Thread(sunshineObject.SunshineLogFileTask);
            //sunshineThread.Start();

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    System.Diagnostics.Debug.WriteLine("Inside workder *******************.");
            //    await Task.Delay(1000, stoppingToken);
            //}
        }


        private Task ParsecTask = new Task(() =>
        {
            ParsecLogsMonitor parsecLogsMonitor = new ParsecLogsMonitor();
        });

        private Task MoonlightTast = new Task(() =>
        {
            NVidiaGamestrem moonlightLogsMonitor = new NVidiaGamestrem();
        });

        private Task SunshineTask = new Task(() =>
        {
            SunshineLogsMonitor sunshineLogsMonitor = new SunshineLogsMonitor();
        });

    } // worker
} // namespace