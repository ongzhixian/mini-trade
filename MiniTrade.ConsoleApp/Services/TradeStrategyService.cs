using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniTrade.ConsoleApp.Models;
using MongoDB.Driver;

namespace MiniTrade.ConsoleApp.Services;

public class TradeStrategyService : BackgroundService
{
    private readonly ILogger<TradeStrategyService> logger;
    private readonly IOptions<MongoDbSetting> options;
    private readonly OandaApiService oandaApi;

    public TradeStrategyService(
        ILogger<TradeStrategyService> logger, 
        IOptions<MongoDbSetting> options,
        OandaApiService oandaApi)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
        this.oandaApi = oandaApi ?? throw new ArgumentNullException(nameof(oandaApi));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation($"{nameof(TradeStrategyService)} is starting.");

        await InitializeAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("do nothing");

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task InitializeAsync(CancellationToken stopToken)
    {
        //IMongoClient mongoClient = new MongoClient(options.Value.ConnectionString);

        //string databaseName = MongoUrl.Create(options.Value.ConnectionString).DatabaseName;

        //var db =  mongoClient.GetDatabase(databaseName);

        //var userCollection = db.GetCollection<UserModel>("user");

        //logger.LogInformation("Record count: {count}", await userCollection.EstimatedDocumentCountAsync(cancellationToken: stopToken));

        logger.LogInformation("In TradeStrategyService, getting account");

        await oandaApi.GetAccountsAsync(stopToken);
    }
}