using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniTrade.ConsoleApp.Models;

namespace MiniTrade.ConsoleApp.Services;

public class StorageTableService : BackgroundService
{
    private readonly ILogger<StorageTableService> logger;
    private readonly IOptions<AzureStorageSetting> options;
    private TableClient tableClient;

    public StorageTableService(ILogger<StorageTableService> logger, IOptions<AzureStorageSetting> options)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation($"{nameof(PubSubConsumerService)} is starting.");

        await InitializeAsync(stoppingToken);

        //if (serviceClient == null)
        //    throw new InvalidOperationException();

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("do nothing");

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task InitializeAsync(CancellationToken stopToken)
    {
        const string tableName = "test";

        TableServiceClient tableServiceClient = new(options.Value.ConnectionString);

        Azure.Response<TableItem>? createTableResponse = await tableServiceClient.CreateTableIfNotExistsAsync(tableName, stopToken);

        if (createTableResponse != null)
            logger.LogInformation("Create table {tableName}", tableName);

        logger.LogInformation("Get table {tableName}", tableName);
        
        tableClient = tableServiceClient.GetTableClient(tableName);

        string partitionKey = "stationaries";
        string rowKey = "k2";

        var entity = new TableEntity(partitionKey, rowKey)
        {
            { "Product", "Marker Set" },
            { "Price", 5.00 },
            { "Quantity", 21 }
        };

        await tableClient.AddEntityAsync(entity, stopToken);

    }
}