using Microsoft.Extensions.Logging;

namespace MiniTrade.ConsoleApp.Services;

public interface IMyService
{
    Task DoWork();
}


internal class MyService : IMyService
{
    private readonly ILogger<MyService> log;

    public MyService(ILogger<MyService> log)
    {
        this.log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public Task DoWork()
    {
        log.LogInformation("Do some work");

        return Task.Run(async () =>
        {
            int i = 0;

            while (true)
            {
                log.LogInformation("Do some work {i}", i++);

                await Task.Delay(3000); // or we can do thread sleep
            }
        });
    }
}
