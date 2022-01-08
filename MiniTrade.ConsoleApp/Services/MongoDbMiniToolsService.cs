using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniTrade.ConsoleApp.Models;
using MongoDB.Driver;

namespace MiniTrade.ConsoleApp.Services;

public class MongoDbMiniToolsService : BackgroundService
{
    private readonly ILogger<MongoDbMiniToolsService> logger;
    private readonly IOptions<MongoDbSetting> options;
    private TableClient tableClient;

    public MongoDbMiniToolsService(ILogger<MongoDbMiniToolsService> logger, IOptions<MongoDbSetting> options)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation($"{nameof(PubSubConsumerService)} is starting.");

        await InitializeAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("do nothing");

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task InitializeAsync(CancellationToken stopToken)
    {
        IMongoClient mongoClient = new MongoClient(options.Value.ConnectionString);
        
        string databaseName = MongoUrl.Create(options.Value.ConnectionString).DatabaseName;
        
        var db =  mongoClient.GetDatabase(databaseName);

        var userCollection = db.GetCollection<UserModel>("user");

        logger.LogInformation("Record count: {count}", await userCollection.EstimatedDocumentCountAsync(cancellationToken: stopToken));
        
    }
}