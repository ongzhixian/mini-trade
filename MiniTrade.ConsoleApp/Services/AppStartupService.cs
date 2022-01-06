using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiniTrade.ConsoleApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrade.ConsoleApp.Services;

internal static class AppStartupService
{
    internal static void ConfigureHost(IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        configurationBuilder.AddEnvironmentVariables("DOTNET_");
        configurationBuilder.AddCommandLine(Environment.GetCommandLineArgs());
    }

    internal static void ConfigureServices(Func<Action<HostBuilderContext, IServiceCollection>, IHostBuilder> configureServices)
    {
        configureServices.Invoke((hostBuilderContext, serviceCollection) =>
        {
            var x = hostBuilderContext.Configuration.GetSection("oanda:account:practice02");

            //hostBuilderContext.Configuration
            serviceCollection.Configure<OandaSettings>(
                "oanda:account:practice02",
                hostBuilderContext.Configuration.GetSection("oanda:account:practice02:api_key")

                //configuration =>
                //{
                //    //hostBuilderContext.Configuration.GetSection()
                //    // messageRouting = MessageRouting.ReadFrom(Configuration);
                //}
            );

            //services.AddHostedService<Worker>();
            // services.AddLogging();
            // services.AddSingleton(Configuration);
            serviceCollection.AddSingleton<IMyService, MyService>();

            serviceCollection.AddHostedService<ExampleHostedService>();

        });
    }

    internal static void ConfigureLogging(Func<Action<HostBuilderContext, ILoggingBuilder>, IHostBuilder> configureLogging)
    {
        configureLogging.Invoke((hostBuilderContext, loggingBuilder) =>
        {
            loggingBuilder.AddConsole();
        });

        //builder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
        //{
        //    loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
        //});
    }

    internal static void ConfigureHost(Func<Action<IConfigurationBuilder>, IHostBuilder> configureHostConfiguration)
    {
        configureHostConfiguration.Invoke(configurationBuilder =>
        {
            configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            configurationBuilder.AddEnvironmentVariables("DOTNET_");
            configurationBuilder.AddCommandLine(Environment.GetCommandLineArgs());
            
        });
    }

    internal static void ConfigureApp(Func<Action<HostBuilderContext, IConfigurationBuilder>, IHostBuilder> configureAppConfiguration)
    {
        configureAppConfiguration.Invoke((hostBuilderContext, configurationBuilder) =>
        {
            IHostEnvironment env = hostBuilderContext.HostingEnvironment;

            configurationBuilder
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("appsettings-secrets.json", optional: true, reloadOnChange: true);

            // Yes, what to do with this? Do we need it?
            // Configuration = configurationBuilder.Build();
            configurationBuilder.Build();
        });
    }
}