using DaireYonetimAPI.Business.Concrete;
using DaireYonetimAPI.DataAccess;
using DaireYonetimAPI.DataAccess.Abstrack;
using DaireYonetimAPI.DataAccess.Concrete;
using DaireYönetimAPI.Entity;
using Moq;

namespace DiareControllerTest
{
    public class Tests
    {
        private readonly DaireDbContext db;

        private  DaireManager manager;


        private Mock<IDaireRepository> mockDaireRepository;
        private Mock<DaireDbContext> mockDbContext;

        [SetUp]
        public void Setup()
        {
            mockDaireRepository = new Mock<IDaireRepository>();
            mockDbContext = new Mock<DaireDbContext>();


            manager = new DaireManager(mockDaireRepository.Object, mockDbContext.Object);
        }


        public Tests(DaireDbContext db, DaireManager manager)
        {
            this.db = db;
            this.manager = manager;
        }

        [Test]
        public void Dairelist_ReturnsAllList_NotNull()
        {
          
           
            // Act
            var DaireList = manager.GetAllDaires();
            // Assert

            Assert.IsNotNull(DaireList);
        }


        [Test]
        public void UpdateDaire_ReturnsUpdate_NonNull()
        {
            // Arrange
           

           

            // Set up mock behavior
            mockDaireRepository.Setup(repo => repo.UpdateDaire(It.IsAny<Daire>()))
                               .Returns(new Daire());

            // Act
            Daire daire = new Daire();
            var daireUpgrade = manager.UpdateDaire(daire);

            // Assert
            Assert.NotNull(daireUpgrade);

        }


        [Test]
        public void UpdateConfigDue_ReturnsConfigControll_NotNull()
        {
            // Arrange
           
            DateTime daires = DateTime.Now;
            string newDue = "200";

            // Act
            if (int.TryParse(newDue, out int daysToAdd))
            {
                Daire daire = new Daire();
                DateTime newDueDate = daires.AddDays(daysToAdd);



                var daireResult = manager.UpdateConfigDue(newDue, newDueDate);

                // Assert
                Assert.NotNull(daireResult);
            }
            else
            {
                Assert.Fail("Error");
            }
        }
    }
}