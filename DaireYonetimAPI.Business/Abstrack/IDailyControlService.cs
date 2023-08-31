using DaireYonetimAPI.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.Business.Abstrack
{
    public class DailyControlService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<DailyControlService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public DailyControlService( ILogger<DailyControlService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private void PerformDailyTaskCallback(object state)
        {
            PerformDailyTask();
        }
        public void PerformDailyTask()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetRequiredService<DaireDbContext>();
               
                    _logger.LogInformation("PerformDailyTask fonksiyonu başladı.");

                    var configMultiplier = GetConfigValue();

                    var negativeBalances = _dbContext.Bakiyes.Where(balance => balance.Paid < 0).ToList();

                    foreach (var balance in negativeBalances)
                    {
                        decimal dailyInterest = CalculateDailyInterest(balance.Paid);
                        decimal configInterest = dailyInterest + configMultiplier;

                        balance.Paid -= dailyInterest + configInterest;

                        _dbContext.SaveChanges();
                        _logger.LogInformation($"Bir hesap güncellendi");
                    }

                    _logger.LogInformation("Tüm günlük işlemler tamamlandı.");
             
            }
        }

        private decimal GetConfigValue()
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetRequiredService<DaireDbContext>();
                var config = _dbContext.Configs.FirstOrDefault();

                if (config != null && decimal.TryParse(config.value, out decimal configValue))
                {
                    return configValue;
                }

                return 0.1m;
            }
        }

        private decimal CalculateDailyInterest(decimal balance)
        {
            decimal dailyInterestRate = 0.01m;
            decimal dailyInterest = balance * dailyInterestRate;
            return dailyInterest;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(PerformDailyTaskCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
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
