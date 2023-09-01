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
        [Test]
        public void testto_daire_list()
        {
            // Arrange
            var db = new DaireDbContext();
            var DaireRepository = new DaireRepository(db);
            var manager = new DaireManager(DaireRepository, db);
            // Act
            var DaireList = manager.GetAllDaires();
            // Assert

            Assert.IsNotNull(DaireList);
        }


        [Test]
        public void UpdateDaire_ReturnsNonNull()
        {
            // Arrange
            var mockDaireRepository = new Mock<IDaireRepository>();
            var mockDbContext = new Mock<DaireDbContext>();

            var manager = new DaireManager(mockDaireRepository.Object, mockDbContext.Object);

            // Set up mock behavior
            mockDaireRepository.Setup(repo => repo.updatedaire(It.IsAny<Daire>()))
                               .Returns(new Daire());

            // Act
            Daire daire = new Daire();
            var daireUpgrade = manager.UpdateDaire(daire);

            // Assert
            Assert.NotNull(daireUpgrade);

        }


        [Test]
        public void UpdateConfigDue_ReturnsNonNull()
        {
            // Arrange
            var mockDbContext = new DaireDbContext();
            var mockDaireRepository = new DaireRepository(mockDbContext);


            var manager = new DaireManager(mockDaireRepository, mockDbContext);

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