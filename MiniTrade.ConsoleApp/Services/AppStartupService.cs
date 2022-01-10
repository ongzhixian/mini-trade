using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiniTrade.ConsoleApp.Models;
using System.Text.Json;

namespace MiniTrade.ConsoleApp.Services;

internal static class AppStartupService
{
    internal static void ConfigureApp(Func<Action<HostBuilderContext, IConfigurationBuilder>, IHostBuilder> configureAppConfiguration)
    {
        configureAppConfiguration.Invoke((_, configurationBuilder) =>
        {
            configurationBuilder.SetBasePath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
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
        configureServices.Invoke((host, services) =>
        {
            RegisterConfigurationInstances(host, services);

            // # RegisterHttpClients(services);

            //services.AddHttpClient();
            services.AddHttpClient<OandaApiService>();

            // # RegisterCaching(services);

            services.AddMemoryCache(); // default IMemoryCache implementation

            // # RegisterServices(services);

            //services.AddSingleton<IMyService, MyService>();

            // # RegisterHostedServices(services);

            //services.AddHostedService<ExampleHostedService>();
            //services.AddHostedService<ExampleBackgroundService>();

            // # Azure (examples)

            //services.AddHostedService<PubSubConsumerService>();
            //services.AddHostedService<PubSubPublisherService>();
            //services.AddHostedService<QueuePublisherService>();
            //services.AddHostedService<QueueConsumerService>();
            //services.AddHostedService<StorageTableService>();
            //services.AddHostedService<StorageBlobService>();

            // # Mongo (examples)

            //services.AddHostedService<MongoDbMiniToolsService>();

            // # Oanda 

            //services.AddHostedService<TradeStrategyService>();

        });
    }

    private static void RegisterConfigurationInstances(HostBuilderContext host, IServiceCollection services)
    {
        services.Configure<AzureStorageSetting>(host.Configuration.GetSection("azure:storage:minitools"));

        services.Configure<AzureWebPubSubSetting>(host.Configuration.GetSection("azure:webpubsub:minitools"));

        services.Configure<MongoDbSetting>(host.Configuration.GetSection("mongodb:minitools"));

        services.Configure<OandaAccountSetting>(o =>
        {
            o.ApiKey = host.Configuration.GetSection("oanda:practice02:apikey").Value;
            o.ApiUrl = host.Configuration.GetSection("Oanda:ApiUrl").Value;
            o.StreamUrl = host.Configuration.GetSection("Oanda:StreamUrl").Value;
        });


#pragma warning disable S125 // Sections of code should not be commented out

        // JsonSerializerOptions

        // Without doing anything, we can inject `JsonSerializerOptions` by doing:

        // IOptions<JsonSerializerOptions> jsonSerializerOptions

        // We can make a custom JsonSerializerOptions as a DI Singleton, like so:

        //services.AddSingleton<JsonSerializerOptions>(new JsonSerializerOptions(JsonSerializerDefaults.Web)
        //{
        //    WriteIndented = false
        //});

        // Or if we prefer to setup configuration instances, we can do something like:

        //services.Configure<JsonSerializerOptions>("web", options =>
        //    options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        //    {
        //        WriteIndented = false
        //    }
        //);

        //services.Configure<JsonSerializerOptions>("general", options =>
        //    options =new JsonSerializerOptions(JsonSerializerDefaults.General)
        //    {
        //        WriteIndented = false
        //    });

        // And inject `IOptionsMonitor<JsonSerializerOptions> optionsMonitor`

#pragma warning restore S125 // Sections of code should not be commented out

    }

    internal static class AppSettingKeys
    {
        public const string AzureStorageMiniTools = "azure:storage:minitools";
        public const string AzureWebPubSubMiniTools = "azure:webpubsub:minitools";
        public const string MongoDbMiniTools = "mongodb:minitools";
        public const string OandaPractice02 = "oanda:practice02";
    }
}