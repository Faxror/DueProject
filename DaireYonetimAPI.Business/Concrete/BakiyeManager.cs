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

        public Bakiye AddDebt(int ApartmentNo, decimal BalanceDue, int id)
        {
            Bakiye bakiye = _dbContext.Bakiyes.Find(id);

            if (bakiye == null)
            {
                bakiye = new Bakiye
                {
                    id = id, 
                    ApartmentNo = ApartmentNo,
                    BalanceDue = BalanceDue,
                    LastPaymentDate = DateTime.Now,
                    PaymentAmount = 0,
                    Faiz = 0,
                    TotalDebt = BalanceDue
                };

                _dbContext.Bakiyes.Add(bakiye);
            }
            else
            {
                bakiye.BalanceDue += BalanceDue;
                bakiye.TotalDebt += BalanceDue;
                bakiye.LastPaymentDate = DateTime.Now;
            }

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.InnerException);


               
                    transaction.Rollback();








                }
            }

            return bakiye;
        }

        public List<BakiyePaymentStatusResponse> PaymentStatus(decimal borc)
        {
            var bakiyeler = _dbContext.Bakiyes
                .Where(c => c.BalanceDue == borc)
                .Select(c => new BakiyePaymentStatusResponse
                {
                    BalanceDue = c.BalanceDue,
                    Faiz = c.Faiz,
                    TotalDebt = c.TotalDebt
                })
                .ToList();

            //decimal faizToplami = bakiyeler.Sum(b => b.Faiz);
            //decimal toplamBorc = bakiyeler.Sum(d => d.ToplamBorc);

            //var response = new Bakiye
            //{
            //    BorcBakiye = borc,
            //    ToplamBorc = toplamBorc,
            //    Faiz = faizToplami
            //};

            return bakiyeler;
        }


        public List<Bakiye> GetAllBakiye()
        {
            return bakiyeRepository.GetAllBakiye();
        }

        public List<BakiyeDebtResponse> GetBakiyeler(bool borcDurumu)
        {
            var Bakiye = _dbContext.Bakiyes.Where(b => b.BalanceDue == 0 && !borcDurumu || b.BalanceDue > 0 && borcDurumu).Select(c => new BakiyeDebtResponse
            {
                BalanceDue = c.BalanceDue,
                TotalDebt = c.TotalDebt,
                ApartmentNo = c.ApartmentNo,
                Faiz = c.Faiz,
                PaymentAmount = c.PaymentAmount


            }).ToList();

                
            return Bakiye;
        }

        public Bakiye calculatecurrentdebt (int apartmentno, decimal paymnet)
        {


            var daire = _dbContext.Daires.FirstOrDefault(d => d.id == apartmentno);

            if (daire == null)
            {
                
                return null;
            }

            if (daire.Bakiye == null)
            {

                Bakiye yeniBakiye = new Bakiye
                {
                    id = apartmentno,
                    BalanceDue = daire.BalanceDue,
                    LastPaymentDate = DateTime.Now,
                    
                    Faiz = 5,
                    TotalDebt = daire.BalanceDue
                };

                _dbContext.Bakiyes.Add(yeniBakiye);
                daire.Bakiye = yeniBakiye;
            }

            
            TimeSpan gecikmeSure = DateTime.Now - daire.Bakiye.LastPaymentDate;
            int gecikmeGun = gecikmeSure.Days;
            decimal faizOrani = 0.0005m;

            if (gecikmeGun > 0)
            {
               
                decimal gecikmeFaiz = daire.Bakiye.BalanceDue * (gecikmeGun * faizOrani);
                daire.Bakiye.Faiz += gecikmeFaiz;
            }

            daire.Bakiye.TotalDebt = daire.Bakiye.BalanceDue + daire.Bakiye.Faiz;
            daire.Bakiye.LastPaymentDate = DateTime.Now;

           
            decimal kalanBorc = daire.Bakiye.BalanceDue - paymnet;
            if (kalanBorc <= 0)
            {
                daire.Bakiye.BalanceDue = 0;
                daire.payduestatus = false;
            }
            else
            {
                daire.Bakiye.BalanceDue = kalanBorc;
            }

            daire.Bakiye.Faiz = 0;
            daire.Bakiye.TotalDebt = daire.Bakiye.BalanceDue + daire.Bakiye.Faiz;
            daire.Bakiye.PaymentAmount = paymnet;

            _dbContext.Entry(daire.Bakiye).State = EntityState.Modified;
            _dbContext.Entry(daire.Bakiye).Property(b => b.ApartmentNo).IsModified = false;


            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return null;
            }

            return daire.Bakiye;


        }

        public List<BakiyeResponse> Payment(int daireId)
        {
            var payments = _dbContext.Bakiyes
                .Where(b => b.ApartmentNo == daireId)
                .Select(b => new BakiyeResponse
                {
                    LastPaymentDate = b.LastPaymentDate,
                    Amountpaid = b.PaymentAmount
                })
                .ToList();

            return payments;
        }
    }
}
