using DaireYönetimAPI.Entity;
using DaireYonetimAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.Business.Abstrack
{
    public interface IBakiyeService
    {
        Bakiye CalculateCurrentDebt(int daireNo, decimal yapilanOdeme);
        //Bakiye AddDebt(int ApartmentNo, decimal BalanceDue, int id);
        List<Bakiye> GetAllBakiye();
        List<BakiyeDebtResponse> GetBakiyeler(bool borcDurumu);
        List<BakiyePaymentStatusResponse> PaymentStatus(decimal borc);

        List<BakiyeResponse> Payment(int daireId);




    }


}
