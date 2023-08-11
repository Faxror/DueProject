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

        Daire cratedaires(Daire daire);

        Daire getdairebyid(int id);

        Daire updatedaire(Daire daire);

        void deletedaire(int id);
    }
}
