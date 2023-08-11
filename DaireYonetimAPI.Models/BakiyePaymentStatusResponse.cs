using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.Models
{
    public class BakiyePaymentStatusResponse
    {
        public decimal BalanceDue { get; set; }
        public decimal Faiz { get; set; }
        public decimal TotalDebt { get; set; }
    }
}
