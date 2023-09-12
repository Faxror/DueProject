using DaireYonetimAPI.DataAccess.Abstrack;
using DaireYönetimAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.DataAccess.Concrete
{
    public class DaireRepository : IDaireRepository
    {
        private readonly DaireDbContext _dbContext;

        public DaireRepository(DaireDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Daire CrateDaires(Daire daire)
        {
            _dbContext.Daires.Add(daire);
            _dbContext.SaveChanges();
            return daire;
        }

        public void DeleteDaire(int id)
        {
            var deleteddaire = GetDaireByid(id);
            _dbContext.Daires.Remove(deleteddaire);
            _dbContext.SaveChanges();

        }

        public List<Daire> GetAllDaires()
        {
            return _dbContext.Daires.ToList();
        }

        public Daire GetDaireByid(int id)
        {
          return _dbContext.Daires.Find(id);
        }

        public Daire UpdateDaire(Daire daire)
        {
            _dbContext.Daires.Update(daire);
            _dbContext.SaveChanges();
            return daire;
        }
    }
}
