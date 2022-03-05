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
            SunshineTask.Start();
            ParsecTask.Start();
            MoonlightTast.Start();
        }

        private Task ParsecTask = new Task(() =>
          {
              ParsecLogsMonitor parsecLogsMonitor = new ParsecLogsMonitor();
          });

        private Task MoonlightTast = new Task(() =>
          {
              NVidiaGamestream moonlightLogsMonitor = new NVidiaGamestream();
          });

        private Task SunshineTask = new Task(() =>
          {
              SunshineLogsMonitor sunshineLogsMonitor = new SunshineLogsMonitor();
          });
    } // worker
} // namespace