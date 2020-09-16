using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ParserGismeteoService.Driver;
using ParserGismeteoService.Driver.Models;
using ParserGismeteoService.Repo;

namespace ParserGismeteoService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly int _intervalTimeout;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _intervalTimeout = appSettings.Value.IntervalTimeout;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var driverScopeService = scope.ServiceProvider.GetRequiredService<IDriverService>();
                    driverScopeService.GetCityGeometrics().Wait();
                }
                _logger.LogInformation("Worker End at: {time}", DateTimeOffset.Now);
                await Task.Delay(_intervalTimeout, stoppingToken);
            }
        }
    }
}
