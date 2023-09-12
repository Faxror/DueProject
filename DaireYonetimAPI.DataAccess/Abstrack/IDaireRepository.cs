using DaireYönetimAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.DataAccess.Abstrack
{
    public interface IDaireRepository
    {
        List<Daire> GetAllDaires();

        Daire CrateDaires(Daire daire);

        Daire GetDaireByid(int id);

        Daire UpdateDaire(Daire daire);

        void DeleteDaire(int id);
    }
}
