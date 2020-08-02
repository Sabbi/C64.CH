using C64.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace C64.FrontEnd
{
    public class StatsUpdater : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer timer;
        private readonly ILogger<StatsUpdater> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private IUnitOfWork unitOfWork;

        public StatsUpdater(ILogger<StatsUpdater> logger, IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StatsUpdater Hosting Service is running");

            timer = new Timer(DoWork, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(30));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StatsUpdater Hosted Service is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            logger.LogInformation("StatsUpdater Hosted Service is DoingWork");
            var sw = new Stopwatch();

            sw.Start();

            var scope = serviceScopeFactory.CreateScope();

            unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            unitOfWork.Productions.UpdateProductionStats();
            unitOfWork.Groups.UpdateGroupStats();

            unitOfWork.Commit().Wait();

            sw.Stop();

            var count = Interlocked.Increment(ref executionCount);
            logger.LogInformation("StatsUpdater Hosted Service finished DoingWork. Count: {Count}, Time: {ElapsedMilliseconds}", count, sw.ElapsedMilliseconds);
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}