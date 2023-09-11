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

        public decimal Paid { get; set; }
        public decimal LastPayment { get; set; }

        // Bakiye ile Daire arasındaki ilişkiyi tanımlıyoruz
        public int? ApartmentId { get; set; }
        public Daire? Daire { get; set; }
    }
}
