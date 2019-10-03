using Library.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Web
{
    public class BackgroundService : IHostedService, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private Timer _timer;

        public BackgroundService(ILogger<BackgroundService> logger, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                //var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
                var sercice = scope.ServiceProvider.GetRequiredService<IBookManager>();
               
                // services
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}