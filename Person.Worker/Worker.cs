using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Person.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Person.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICustomerRepository _customerRepository;

        public Worker(ILogger<Worker> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;

                if (now.Hour == 0 && now.Minute == 0)
                {
                    var birthdayCustomers = await _customerRepository.GetCustomersWithBirthdayTodayAsync();

                    foreach (var customer in birthdayCustomers)
                    {
                        _logger.LogInformation($"🎉 Happy Birthday {customer.Name} {customer.LastName}!");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
