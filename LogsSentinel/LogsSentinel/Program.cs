using LogsSentinel;
using Serilog;
using Serilog.Events;


// overriding the windows logger and using serilog
string LogsSentinelDir = AppDomain.CurrentDomain.BaseDirectory;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(LogsSentinelDir + @"\LogsSentinelLogs.txt")
    .CreateLogger();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();


// executing the script
try
{
    Log.Information("Starting up the LogsSentinel");
    await host.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "There was a problem starting the LogsSentinel");
}
finally
{
    Log.CloseAndFlush();
}

