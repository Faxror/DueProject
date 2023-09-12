using DaireYonetimAPI.DataAccess;
using DaireYönetimAPI.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.Business.Abstrack
{
    public class EmailSenderService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(IServiceScopeFactory scopeFactory, ILogger<EmailSenderService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendEmails, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        public void SendEmails(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetRequiredService<DaireDbContext>();
                var recipients = _dbContext.Bakiyes.Where(b => b.Paid < 0).ToList();

              


                var stmp = _dbContext.Configs.FirstOrDefault() ?? throw new Exception();

                using (SmtpClient smtpClient = new SmtpClient(stmp.SmptSenderServers))
                {

                    
                        smtpClient.Port = 587;
                        smtpClient.Credentials = new NetworkCredential(stmp.SmptEmailAddress, stmp.SmptEmailPassword);
                        smtpClient.EnableSsl = true;

                        foreach (var recipient in recipients)
                        {
                            
                          var joinedRecipient = (from daire in _dbContext.Daires
                                                   join bakiye in _dbContext.Bakiyes on daire.BakiyeId equals bakiye.id
                                                   where daire.id == recipient.ApartmentId 
                                                   select new
                                                   {
                                                       Daire = daire,
                                                       Bakiye = bakiye
                                                   }).FirstOrDefault() ?? throw new Exception();

                           
                            decimal currentBalance = recipient.Paid;
                            string emailAddress = joinedRecipient.Daire.apartmentemail;
                            string familyName = joinedRecipient.Daire.familyname;
                            string emailTitle = "Fax Apartment Aidat Ödeme Hatırlatması";
                            string emailBody = $"Merhaba Sayın Apartman Üyesi, {familyName} Ailesi\n\n"
                                + "Nasılsınız? Aidet Ödemiyorsunuz ama bizi üzüyorsunuz :/\n\n"
                                + $"Güncel Hesap Borcunuz: {currentBalance:C}\n\n"
                                + "Lütfen Ödeyin.... lütfen";

                            MailMessage mail = new MailMessage(stmp.SmptSenderUsers, emailAddress, stmp.SmptUsersMailTitle, emailBody);
                        
                            smtpClient.Send(mail);
                            _logger.LogInformation($"Başarılı Şekilde Gönderildi. Gönderilen Mail {emailAddress}. Gönderilen Aile {familyName}");
                    }
                }
            }
        }

        public string GetRecipientEmail(int apartmentId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DaireDbContext>();
                var daire = dbContext.Daires.FirstOrDefault(d => d.id == apartmentId);

                return daire?.apartmentemail;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
