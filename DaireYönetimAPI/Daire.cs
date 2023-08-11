using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYönetimAPI.Entity
{
    public class Daire
    {
        [Key]
        public int id { get; set; }

        public int apartmentno { get; set; }

        public string apartmentname { get; set; }

        public string apartmentemail { get; set; }

        public string apartmentdues { get; set; }
        public string dateofvalidity { get; set; }

        public bool payduestatus { get; set; }

        public decimal  BalanceDue { get; set; }
        public DateTime PaymentdueDate { get; set; }

        public int? BakiyeId { get; set; }
        public Bakiye? Bakiye { get; set; }
    }
}
