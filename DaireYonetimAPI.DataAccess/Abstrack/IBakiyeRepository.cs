using DaireYönetimAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.DataAccess.Abstrack
{
    public interface IBakiyeRepository
    {
        List<Bakiye> GetAllBakiye();

        Bakiye GetByBakiyeİd(int id);


    }
}
