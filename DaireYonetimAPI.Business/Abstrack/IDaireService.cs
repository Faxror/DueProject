using DaireYönetimAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.Business.Abstrack
{
    public interface IDaireService
    {
        List<Daire> GetAllDaires();

        Daire CreateDaires(Daire daireInput);




        Daire GetDaireByİD(int id);

        Daire UpdateDaire(Daire daire);

        void DeleteDaire(int id);
    }
}
