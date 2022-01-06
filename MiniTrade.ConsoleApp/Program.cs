using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiniTrade.ConsoleApp.Services;
using AppStartup = MiniTrade.ConsoleApp.Services.AppStartupService;

HostBuilder builder = new HostBuilder();

AppStartup.ConfigureHost(builder.ConfigureHostConfiguration);

AppStartup.ConfigureApp(builder.ConfigureAppConfiguration);

AppStartup.ConfigureLogging(builder.ConfigureLogging);

AppStartup.ConfigureServices(builder.ConfigureServices);

using (ILoggerFactory loggerFactory = new LoggerFactory())
using (IHost host = builder.Build())
{
    IServiceProvider services = host.Services;

    ILogger log = services.GetRequiredService<ILogger<Program>>();

    try
    {
        log.LogInformation("Application start");

        IMyService myService = services.GetRequiredService<IMyService>();

        await host.RunAsync();
    }
    catch (Exception ex)
    {
        log.LogError(ex, string.Empty);
        throw;
    }
}
