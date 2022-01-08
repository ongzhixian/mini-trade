using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MiniTrade.ConsoleApp.Services;

internal class ExampleHostedService : IHostedService
{
    private readonly ILogger<ExampleHostedService> logger;

    private CancellationToken cancellationToken;

    public ExampleHostedService(ILogger<ExampleHostedService> logger, IHostApplicationLifetime appLifetime)
    {
        this.logger = logger;

        appLifetime.ApplicationStarted.Register(OnStarted);

        appLifetime.ApplicationStopping.Register(OnStopping);

        appLifetime.ApplicationStopped.Register(OnStopped);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("1. StartAsync has been called.");

        this.cancellationToken = cancellationToken;

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("4. StopAsync has been called.");

        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        logger.LogInformation("2. OnStarted has been called.");

        Task.Run(async () =>
        {
            await DoWorkAsync(cancellationToken);
        }, cancellationToken);
    }

    private void OnStopping()
    {
        logger.LogInformation("3. OnStopping has been called.");
    }

    private void OnStopped()
    {
        logger.LogInformation("5. OnStopped has been called.");
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            logger.LogInformation("{serviceClass} task doing background work.", nameof(ExampleHostedService));

            // do some work

            await Task.Delay(1000, cancellationToken);
        }
    }
}
