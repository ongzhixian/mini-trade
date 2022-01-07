using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiniTrade.ConsoleApp.Services;
using AppStartup = MiniTrade.ConsoleApp.Services.AppStartupService;

IHostBuilder builder = Host.CreateDefaultBuilder(args);

Console.Write("Configuring application...");

AppStartup.ConfigureApp(builder.ConfigureAppConfiguration);

#pragma warning disable S125 // Sections of code should not be commented out
// AppStartup.ConfigureLogging(builder.ConfigureLogging);
#pragma warning restore S125 // Sections of code should not be commented out

AppStartup.ConfigureServices(builder.ConfigureServices);

Console.WriteLine("OK");

using (IHost host = builder.Build())
{
    IServiceProvider services = host.Services;

    ILogger log = services.GetRequiredService<ILogger<Program>>();

    try
    {
        log.LogInformation("Application start");

#pragma warning disable S125 // Sections of code should not be commented out
        // services.GetRequiredService<IMyService>().DoWork();
#pragma warning restore S125 // Sections of code should not be commented out
        
        await host.RunAsync();
    }
    catch (Exception ex)
    {
        log.LogError(ex, "Caught exception at root.");
        throw;
    }
}
