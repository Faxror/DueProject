


using DaireYonetimAPI.Business.Abstrack;
using DaireYonetimAPI.Business.Concrete;
using DaireYonetimAPI.DataAccess;
using DaireYonetimAPI.DataAccess.Abstrack;
using DaireYonetimAPI.DataAccess.Concrete;
using DaireYönetimAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace BakiyeSerivceTest.cs
{
    public class BakiyeServiceTest
    {


        [Test]
        public void BakiyeTo_ListTo_Fix()
        {
            var db = new DaireDbContext();
            var bakiyeRepository = new BakiyeRepository(db);

            var manager = new BakiyeManager(db, bakiyeRepository);
            var bakiyeList = manager.GetAllBakiye();

            Assert.IsTrue(bakiyeList.Count > 0);

            foreach (var bakiye in bakiyeList)
            {
                Assert.IsNotNull(bakiye);
               
            }
        }


        [Test]
        public void Bakiye_AddToDebt()
        {
            var db = new DaireDbContext();
            var bakiyeRepository = new BakiyeRepository(db);

            var manager = new BakiyeManager(db, bakiyeRepository);
            var service = new Bakiye()
            {
                ApartmentNo = 11,
            };

            int aperno = 11;
            decimal balancedue = 100;
            int id = 1;

            var bakiyeadd = manager.AddDebt(aperno, balancedue, id);

            Assert.IsNotNull(bakiyeadd);
            
            
        }
    }

}

   