using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MiniTrade.ConsoleApp.Services;

internal class ExampleBackgroundService : BackgroundService
{
    private readonly ILogger<ExampleBackgroundService> logger;

    public ExampleBackgroundService(ILogger<ExampleBackgroundService> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("{serviceClass} is starting.", nameof(ExampleBackgroundService));

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("{serviceClass} task doing some work.", nameof(ExampleBackgroundService));

            // do some work here

            await Task.Delay(1000, stoppingToken);
        }
    }
}
