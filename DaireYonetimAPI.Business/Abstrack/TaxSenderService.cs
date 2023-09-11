using DaireYonetimAPI.DataAccess;
using DaireYönetimAPI.Entity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.Business.Abstrack
{
    public class TaxSenderService : IHostedService, IDisposable
    {
        private  Timer _timer;
        private readonly ILogger<TaxSenderService> _logger;
        private readonly DaireDbContext dbContext;

        public TaxSenderService(ILogger<TaxSenderService> logger, DaireDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }


        public void SenderTax(object state)
        {

            var Paid = dbContext.Bakiyes.FirstOrDefault() ?? throw new Exception();

            DateTime now = DateTime.UtcNow;
            DateTime lastPaymentDate = new DateTime(now.Year, now.Month, 23, 0, 0, 0, DateTimeKind.Utc);

            if (now.Day > 23)
            {
                lastPaymentDate = lastPaymentDate.AddMonths(1);
            }



            TimeSpan gecikmeSure = now - lastPaymentDate;
            int gecikmeGun = gecikmeSure.Days;
            decimal faizOrani = 0.01m;

            if (Paid.Paid > 0 && gecikmeGun > 0 && now.Day > 23)
            {
                decimal gecikmeFaiz = Paid.Paid * (gecikmeGun * faizOrani);
                Paid.Paid += gecikmeFaiz;
            }            //Coming soon.....
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SenderTax, null, TimeSpan.Zero, TimeSpan.FromDays(23));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
