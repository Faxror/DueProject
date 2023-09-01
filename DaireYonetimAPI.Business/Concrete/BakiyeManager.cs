using DaireYonetimAPI.Business.Abstrack;
using DaireYonetimAPI.DataAccess;
using DaireYonetimAPI.DataAccess.Abstrack;
using DaireYönetimAPI.Entity;
using DaireYonetimAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        public Bakiye calculatecurrentdebt(int apartmentNo, decimal payment)
        {
            var bakiye = _dbContext.Bakiyes.FirstOrDefault(b => b.Daire.id == apartmentNo);

            if (bakiye == null)
            {
                return null;
            }

            var config = _dbContext.Configs.FirstOrDefault();

            if (config == null)
            {
                return null;
            }

            DateTime now = DateTime.UtcNow; 
            DateTime lastPaymentDate = new DateTime(now.Year, now.Month, 23, 0, 0, 0, DateTimeKind.Utc);

            if (now.Day > 23)
            {
                lastPaymentDate = lastPaymentDate.AddMonths(1);
            }

            TimeSpan gecikmeSure = now - lastPaymentDate;
            int gecikmeGun = gecikmeSure.Days;
            decimal faizOrani = 0.01m;

            if (bakiye.Paid > 0 && gecikmeGun > 0)
            {
                decimal gecikmeFaiz = bakiye.Paid * (gecikmeGun * faizOrani);
                bakiye.Paid += gecikmeFaiz;
            }

            config.ModifiedDate = lastPaymentDate;

            decimal paymentAmount = payment;

            if (bakiye.Paid < 0 && paymentAmount > 0)
            {
                if (paymentAmount <= Math.Abs(bakiye.Paid))
                {
                    bakiye.Paid += paymentAmount;
                }
                else
                {
                    bakiye.Paid = 0;
                }
            }

            try
            {
                _dbContext.Entry(bakiye).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
               
                Console.WriteLine("Veritabanı güncelleme hatası: " + ex.Message);
                return null;
            }

           
           
            return bakiye;
        }


        public List<BakiyeResponse> Payment(int daireId)
        {
            var payments = _dbContext.Bakiyes
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
