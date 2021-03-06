using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniTrade.ConsoleApp.Models;

namespace MiniTrade.ConsoleApp.Services;

public class QueuePublisherService : BackgroundService
{
    private readonly ILogger<QueuePublisherService> logger;
    private readonly IOptions<AzureStorageSetting> options;
    QueueClient? queueClient;

    public QueuePublisherService(ILogger<QueuePublisherService> logger, IOptions<AzureStorageSetting> options)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation($"{nameof(QueuePublisherService)} is starting.");

        await InitializeAsync(stoppingToken);

        if (queueClient == null)
            throw new InvalidOperationException();

        while (!stoppingToken.IsCancellationRequested)
        {
            string message = $"{DateTime.Now} ExampleService message";

            SendReceipt receipt = await queueClient.SendMessageAsync(message, stoppingToken);

            logger.LogInformation("Sent: [{message}]", message);

            await Task.Delay(10000, stoppingToken);
        }
    }

    private async Task InitializeAsync(CancellationToken stopToken)
    {
        const string queueName = "test-queue1";

        queueClient = new QueueClient(options.Value.ConnectionString, queueName);

        var queueExist = await queueClient.ExistsAsync(stopToken);

        if (queueExist)
        {
            logger.LogInformation($"Using existing queue: {queueName}");

            return;
        }

        logger.LogInformation($"Creating queue: {queueName}");

        await queueClient.CreateAsync(cancellationToken: stopToken);
    }
}