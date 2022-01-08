using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniTrade.ConsoleApp.Models;
using MiniTrade.ConsoleApp.Models.Oanda;
using MiniTrade.ConsoleApp.Services;
using System.Text.Json;
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

        //var a1 = services.GetRequiredService<IOptions<AzureStorageSetting>>();
        //var a2 = services.GetRequiredService<IOptions<AzureWebPubSubSetting>>();
        //var a3 = services.GetRequiredService<IOptions<MongoDbSetting>>();
        //var a4 = services.GetRequiredService<IOptions<OandaAccountSetting>>();

        //log.LogInformation("{a1}", a1.Value.ConnectionString);
        //log.LogInformation("{a2}", a2.Value.ConnectionString);
        //log.LogInformation("{a3}", a3.Value.ConnectionString);
        //log.LogInformation("The A4 {a4}, {a}, {b}", a4.Value.ApiKey, a4.Value.ApiUrl, a4.Value.StreamUrl);

        //var prop = new AccountProperties();
        //prop.Id = "asd";
        //prop.Mt4AccountID = 123123;
        //prop.Tags = new List<string>()
        //{
        //    "asd", "zxc"
        //};

        //AccountsResponse response = new AccountsResponse();
        //response.Accounts = new List<AccountProperties>()
        //{
        //    prop
        //};


        //var json = JsonSerializer.Serialize< AccountsResponse>(response);
        //Console.WriteLine(json);

        try
        {
            var r = services.GetRequiredService<OandaApiService>();

            if (r != null)
            {
                Console.WriteLine("SERVICE EXISTS");
            }
            else
            {
                Console.WriteLine("SERVICE MISSING");
            }

            await r.GetAccountsAsync(CancellationToken.None);
        }
        catch (Exception ex)
        {
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine(ex);

        }




        await host.RunAsync();
    }
    catch (Exception ex)
    {
        //log.LogError(ex, "Caught exception at root.");
        Console.Error.WriteLine(ex);
        throw;
    }
}
