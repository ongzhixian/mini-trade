using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrade.ConsoleApp.Services
{
    internal class ExampleHostedService : IHostedService
    {
        ILogger<ExampleHostedService> logger;

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

            return Task.CompletedTask;

            //return Task.Run(async () =>
            //{
            //    int i = 0;

            //    while (true)
            //    {
            //        logger.LogInformation("Do some work {i}", i++);

            //        //System.Threading.Thread.Sleep(1000);

            //        await Task.Delay(1000);
            //    }
            //});

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("4. StopAsync has been called.");

            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            logger.LogInformation("2. OnStarted has been called.");
        }

        private void OnStopping()
        {
            logger.LogInformation("3. OnStopping has been called.");
        }

        private void OnStopped()
        {
            logger.LogInformation("5. OnStopped has been called.");
        }
    }
}
