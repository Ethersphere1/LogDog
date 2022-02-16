using LogsSentinel;
using Serilog;
using Serilog.Events;


// overriding the windows logger and using serilog
string LogsSentinelDir = AppDomain.CurrentDomain.BaseDirectory;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(LogsSentinelDir + @"\LogDog.txt")
    .CreateLogger();


IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
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

