using Serilog;
using Serilog.Events;

// overriding the windows logger and using serilog
string LogsSentinelDir = AppDomain.CurrentDomain.BaseDirectory;
string currUserDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
string ethersphereinDocs = Path.Combine(currUserDocuments, "Ethersphere");
string logDoginDocs = Path.Combine(ethersphereinDocs, "LogDog");

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
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(Path.Combine(logDoginDocs, "logs.txt"), rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 70000000)
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<LogDog.Worker>();
    })
    .UseSerilog()
    .Build();

// executing the script
try
{
    Log.Information("Starting up the LogDog");
    await host.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "There was a problem starting the LogDog");
}
finally
{
    Log.CloseAndFlush();
}