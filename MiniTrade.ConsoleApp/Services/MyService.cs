using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniTrade.ConsoleApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrade.ConsoleApp.Services;

public interface IMyService
{
    Task DoWork();
}


internal class MyService : IMyService
{
    private readonly ILogger<MyService> log;

    public MyService(ILogger<MyService> log, IOptionsMonitor<OandaSettings> optionsMonitor)
    {
        this.log = log ?? throw new ArgumentNullException(nameof(log));

        OandaSettings? x = optionsMonitor.Get("oanda:account:practice02");

        if (x != null)
        {
            Console.WriteLine(x.apikey);
        }
        else
        {
            Console.WriteLine("no api key");
        }

        DoWork();
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

                //System.Threading.Thread.Sleep(1000);

                await Task.Delay(1000);
            }
        });
    }
}
