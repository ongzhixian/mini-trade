using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MiniTrade.ConsoleApp.Services;

internal class ExampleEventEmitterService : BackgroundService
{
    private readonly ILogger<ExampleEventEmitterService> logger;

    // Subscription (OnEvent)
    public static event EventHandler? Notify;

    public ExampleEventEmitterService(ILogger<ExampleEventEmitterService> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        //TriggerEvent += ExampleEventEmitterService_TriggerEvent;
    }

    public static void Test()
    {
        if (Notify != null)
            Notify(null, new EventArgs());
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("{serviceClass} is starting.", nameof(ExampleEventEmitterService));

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("{serviceClass} task doing some work.", nameof(ExampleEventEmitterService));

            ExampleEventEmitterService.Test();

            await Task.Delay(1000, stoppingToken);
        }
    }

    //public static void ExampleEventEmitterService_TriggerEvent(object? sender, EventArgs e)
    //{
    //    //logger.LogInformation("Fire event");
    //    Console.WriteLine("FireEvent");
    //}

    //protected virtual void OnThresholdReached(EventArgs e)
    //{
    //    EventHandler handler = ThresholdReached;
    //    handler?.Invoke(this, e);
    //}

    //protected static void FireThresholdReached(object sender, EventArgs e)
    //{
    //    //EventHandler handler = ThresholdReached;
    //    //handler?.Invoke(sender, e);
    //    ThresholdReached?.Invoke(sender, e);
    //}
}
