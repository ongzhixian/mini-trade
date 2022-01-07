using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiniTrade.ConsoleApp.Services;

internal static class AppStartupService
{
    internal static void ConfigureApp(Func<Action<HostBuilderContext, IConfigurationBuilder>, IHostBuilder> configureAppConfiguration)
    {
        configureAppConfiguration.Invoke((_, configurationBuilder) =>
        {
            configurationBuilder.AddJsonFile("appsecrets.json", optional: true, reloadOnChange: true);
        });
    }

#pragma warning disable S125 // Sections of code should not be commented out
    //internal static void ConfigureLogging(Func<Action<HostBuilderContext, ILoggingBuilder>, IHostBuilder> configureLogging)
    //{
    //    //builder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
    //    //{
    //    //    loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
    //    //});
    //}
#pragma warning restore S125 // Sections of code should not be commented out

    internal static void ConfigureServices(Func<Action<HostBuilderContext, IServiceCollection>, IHostBuilder> configureServices)
    {
        configureServices.Invoke((_, services) =>
        {
            services.AddSingleton<IMyService, MyService>();

            services.AddHostedService<ExampleHostedService>();

            services.AddHostedService<ExampleBackgroundService>();

        });
    }
}