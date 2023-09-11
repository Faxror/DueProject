using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYönetimAPI.Entity
{
    public class Config
    {
        public int Id { get; set; }
        
        public string  value { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string SmptEmailAddress { get; set; }
        public string SmptEmailPassword { get; set; }
        public string SmptSenderUsers { get; set; }
        public string SmptSenderServers { get; set; }
        public string SmptUsersMailTitle { get; set; }
        public decimal TotalLastPayment { get; set; }
    }
}
