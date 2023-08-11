using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYönetimAPI.Entity
{
    public class Bakiye
    {
        [Key]
        public int id { get; set; }

        public int ApartmentNo { get; set; }
        public decimal BalanceDue { get; set; }
        public decimal Faiz { get; set; }
        public decimal TotalDebt { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }

        // Bakiye ile Daire arasındaki ilişkiyi tanımlıyoruz
        public int? DaireId { get; set; }
        public Daire? Daire { get; set; }
    }
}
