using DaireYonetimAPI.DataAccess.Abstrack;
using DaireYönetimAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaireYonetimAPI.DataAccess.Concrete
{
    public class BakiyeRepository : IBakiyeRepository
    {
        private readonly DaireDbContext _db;

        public BakiyeRepository(DaireDbContext db)
        {
            _db = db;
        }

        public Bakiye bakiyeguncelle()
        {
            throw new NotImplementedException();
        }

        public Bakiye bakiyekle()
        {
            throw new NotImplementedException();
        }

        public Bakiye bakiyesil()
        {
            throw new NotImplementedException();
        }

        public List<Bakiye> GetAllBakiye()
        {
           return _db.Bakiyes.ToList();
        }

        public Bakiye GetByBakiyeİd(int id)
        {
            return _db.Bakiyes.Find(id);
        }
    }
}
