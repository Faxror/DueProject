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

        public TaxSenderService(ILogger<TaxSenderService> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }


        public void SenderTax(object state)
        {
            //Coming soon.....
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
