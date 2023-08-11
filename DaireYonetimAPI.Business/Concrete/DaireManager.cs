using DaireYonetimAPI.Business.Abstrack;
using DaireYonetimAPI.DataAccess;
using DaireYonetimAPI.DataAccess.Abstrack;
using DaireYönetimAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc;

namespace DaireYonetimAPI.Business.Concrete
{
    public class DaireManager : IDaireService
    {
        private readonly IDaireRepository _daireRepository;
        private readonly DaireDbContext _dbContext;

        public DaireManager(IDaireRepository daireRepository, DaireDbContext dbContext)
        {
            _daireRepository = daireRepository;
            _dbContext = dbContext;
        }

        public Daire CreateDaires(Daire daireInput)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var newBakiye = new Bakiye
                {
                    id = daireInput.id,
                    ApartmentNo = daireInput.apartmentno,
                    BalanceDue = daireInput.BalanceDue,
                    Faiz = 0,
                    TotalDebt = daireInput.BalanceDue,
                    LastPaymentDate = daireInput.PaymentdueDate,
                    PaymentAmount = 0
                };

                _dbContext.Bakiyes.Add(newBakiye);
                _dbContext.SaveChanges();

                var newDaire = new Daire
                {
                    id = daireInput.id,
                    apartmentno = daireInput.apartmentno,
                    apartmentname = daireInput.apartmentname,
                    apartmentemail = daireInput.apartmentemail,
                    apartmentdues = daireInput.apartmentdues,
                    dateofvalidity = daireInput.dateofvalidity,
                    payduestatus = daireInput.payduestatus,
                    BalanceDue = daireInput.BalanceDue,
                    PaymentdueDate = daireInput.PaymentdueDate,
                    BakiyeId = newBakiye.id
                };

                _dbContext.Daires.Add(newDaire);
                _dbContext.SaveChanges();

                transaction.Commit();

                return newDaire;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.InnerException); 
                return null;
            }
        }
    

        public void DeleteDaire(int id)
        {
            var daire = GetDaireByİD(id);
            if (daire != null) {
                _daireRepository.deletedaire(id);
                    }
        }

        public List<Daire> GetAllDaires()
        {
            return _daireRepository.GetAllDaires();
        }

        public Daire GetDaireByİD(int id)
        {
           return _daireRepository.getdairebyid(id);
        }

        public Daire UpdateDaire(Daire daire)
        {
            return _daireRepository.updatedaire(daire);
        }
    }
}
