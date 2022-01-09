using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniTrade.ConsoleApp.Helpers;
using MiniTrade.ConsoleApp.Models;
using MiniTrade.ConsoleApp.Models.Oanda;
using MiniTrade.ConsoleApp.Services;
using System.Text;
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

    IMemoryCache cache = services.GetRequiredService<IMemoryCache>();


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
        
        var api = services.GetRequiredService<OandaApiService>();

        //string outputPath = "cache/sample-cache.json";
        
        //AccountsResponse? accounts;

        //// Load data from data file (if it exists)
        //accounts = await FileHelper.LoadFileAsync<AccountsResponse>(outputPath);

        //// If data is not available, pull
        //if (accounts == null)
        //{
        //    try
        //    {
        //        accounts = await api.GetAccountsAsync(CancellationToken.None);

        //        FileHelper.SaveToFile<AccountsResponse>(outputPath, accounts);

        //        //if ((accounts != null) && (accounts.Accounts != null))
        //        //{
        //        //    log.LogInformation("{accounts}", accounts.Accounts.Count());

        //        //    foreach (var account in accounts.Accounts)
        //        //    {
        //        //        log.LogInformation("{Id}, {Mt4AccountID}",
        //        //            account.Id, account.Mt4AccountID);
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}


        //string json = await FileHelper.LoadFileAsync(outputPath);
        // //AccountsResponse? accounts = cache.Get<AccountsResponse>("oandaAccounts");

        //if (accounts != null)
        //    cache.Set<AccountsResponse>("oandaAccounts", accounts, 
        //        TimeSpan.FromSeconds(5));

        //await api.GetAccountAsync("101-003-11976008-002", CancellationToken.None);

        await api.GetAccountInstrumentsAsync("101-003-11976008-002", CancellationToken.None);

        await host.RunAsync();
    }
    catch (Exception ex)
    {
        //log.LogError(ex, "Caught exception at root.");
        Console.Error.WriteLine(ex);
        throw;
    }
}
