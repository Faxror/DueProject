using DaireYonetimAPI.Business.Abstrack;
using DaireYonetimAPI.DataAccess;
using DaireYonetimAPI.DataAccess.Abstrack;
using DaireYönetimAPI.Entity;
using DaireYonetimAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DaireYonetimAPI.Business.Concrete
{
    public class BakiyeManager : IBakiyeService
    {
        private readonly DaireDbContext _dbContext;
        private readonly IBakiyeRepository bakiyeRepository;
        private readonly ILogger<BakiyeManager> logger;

        public BakiyeManager(DaireDbContext dbContext, IBakiyeRepository bakiyeRepository)
        {
            _dbContext = dbContext;
            this.bakiyeRepository = bakiyeRepository;
        }

        //public Bakiye AddDebt(int ApartmentNo, decimal BalanceDue, int id)
        //{
        //    Bakiye bakiye = _dbContext.Bakiyes.Find(id);

        //    if (bakiye == null)
        //    {
        //        bakiye = new Bakiye
        //        {
        //            id = id,
        //            ApartmentNo = ApartmentNo,
        //            Paid = BalanceDue
        //        };

        //        _dbContext.Bakiyes.Add(bakiye);
        //    }
        //    else
        //    {
        //        bakiye.Paid += BalanceDue;

        //    }

        //    using (var transaction = _dbContext.Database.BeginTransaction())
        //    {
        //        try
        //        {

        //            _dbContext.SaveChanges();
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {

        //            Console.WriteLine(ex.InnerException);



        //            transaction.Rollback();








        //        }
        //    }

        //    return bakiye;
        //}

        public List<BakiyePaymentStatusResponse> PaymentStatus(decimal borc)
        {
            var bakiyeler = _dbContext.Bakiyes
                .Where(c => c.Paid == borc)
                .Select(c => new BakiyePaymentStatusResponse
                {
                    BalanceDue = c.Paid,
                   
                })
                .ToList();
            //decimal faizToplami = bakiyeler.Sum(b => b.Faiz);
            //decimal toplamBorc = bakiyeler.Sum(d => d.ToplamBorc);
            
            return bakiyeler;
        }
        public List<Bakiye> GetAllBakiye()
        {
            return bakiyeRepository.GetAllBakiye();
        }

        public List<BakiyeDebtResponse> GetBakiyeler(bool borcDurumu)
        {
            var Bakiye = _dbContext.Bakiyes.Where(b => b.Paid == 0 && !borcDurumu || b.Paid > 0 && borcDurumu).Select(c => new BakiyeDebtResponse
            {
                 ApartmentNo = c.ApartmentNo,
                 Paid = c.Paid,
            }).ToList();
            return Bakiye;
        }

        public Bakiye CalculateCurrentDebt(int apartmentNo, decimal payment)
        {
            var bakiye = _dbContext.Bakiyes.FirstOrDefault(b => b.Daire.id == apartmentNo);

            if (bakiye == null)
            {
                return null;
            }

            var config = _dbContext.Configs.FirstOrDefault();


            DateTime now = DateTime.UtcNow; 
            DateTime lastPaymentDatee = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, DateTimeKind.Utc);

            config.ModifiedDate = lastPaymentDatee;

            decimal paymentAmount = payment;

            bakiye.LastPayment += payment;

            config.TotalLastPayment += payment;

            if (bakiye.Paid < 0 && paymentAmount > 0)
            {
                if (paymentAmount <= Math.Abs(bakiye.Paid))
                {
                    bakiye.Paid += paymentAmount;
                }
                else // 10 günlük 100 bin tl
                {
                    bakiye.Paid = 0;
                }
            }

            
                _dbContext.Entry(bakiye).State = EntityState.Modified;
                _dbContext.SaveChanges();
            
            return bakiye;
        }

        public List<BakiyeResponse> Payment(int daireId)
        {
            List<BakiyeResponse> payments = _dbContext.Bakiyes
                .Where(b => b.ApartmentNo == daireId)
                .Select(b => new BakiyeResponse
                {                    
                    Amountpaid = b.Paid,
                })
                .ToList();
            return payments;
        }
    }
}
