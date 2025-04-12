//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Person.Api.Services
//{
//    public class BirthdayCheckerService : BackgroundService
//    {
//        private readonly ILogger<BirthdayCheckerService> _logger;
//        private readonly IServiceProvider _serviceProvider;
//        public BirthdayCheckerService(
//            ILogger<BirthdayCheckerService> logger)
//        {
//            _logger = logger;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                var now = DateTime.Now;

//                if (now.Hour == 0 && now.Minute == 0) // Midnight check
//                {
//                    //var birthdayCustomers = await GetCustomersWithBirthdayTodayAsync();
//                    //foreach (var customer in birthdayCustomers)
//                    //{
//                        _logger.LogInformation($"🎉 Happy Birthday");
//                    //}
//                }

//                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); 
//            }
//        }
//    }
//}
