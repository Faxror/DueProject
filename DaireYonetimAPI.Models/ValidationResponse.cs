using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.Models
{
    public class ValidationResponse<T>
    {
        public bool Success { get; set; }

        public List<string> Message { get; set; }

        public T Data { get; set; }

        public int ApartmentNo { get; set; }
        public decimal Paid { get; set; }
    }
}
