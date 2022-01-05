
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IConfigurationRoot Configuration = null;

HostBuilder builder = new HostBuilder();

Host.CreateDefaultBuilder(args);


builder.UseContentRoot(Directory.GetCurrentDirectory());

//builder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
//{
//    loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
//});

builder.ConfigureHostConfiguration(configurationBuilder =>
{
    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
    configurationBuilder.AddEnvironmentVariables("DOTNET_");
    configurationBuilder.AddCommandLine(args);
});

builder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
{
    IHostEnvironment env = hostBuilderContext.HostingEnvironment;

    Console.WriteLine(env.EnvironmentName);

    configurationBuilder
        .AddJsonFile("appsettings.json", optional: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddJsonFile("appsettings-secret.json", optional: true, reloadOnChange: true);

    Configuration = configurationBuilder.Build();
});

builder.ConfigureLogging((hostingContext, logging) =>
{
    // logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logging.ClearProviders();
    logging.AddConsole();
});


builder.ConfigureServices((hostBuilderContext, services) =>
{
    //services.AddHostedService<Worker>();
    // services.AddLogging();
    // services.AddSingleton(Configuration);
    //services.AddSingleton<IMyService, MyService>();

});

// var builder = WebApplication.CreateBuilder(args);

// var app = builder.Build();

await builder.Build().RunAsync();


// using (ILoggerFactory loggerFactory = new LoggerFactory())
// using (IHost host = builder.Build())
// {
//     IServiceProvider services = host.Services;

//     ILogger log = services.GetRequiredService<ILogger<Program>>();

//     log.LogInformation("Application start");

//     try
//     {
//         log.LogInformation("Application start");

//         Console.WriteLine("Lets start");

//         await host.RunAsync();
//     }
//     catch (Exception ex)
//     {
//         //log.LogError(ex, string.Empty);
//         throw;
//     }
// }