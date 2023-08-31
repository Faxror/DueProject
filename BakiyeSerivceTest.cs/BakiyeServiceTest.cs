
using DaireYonetimAPI.Business.Abstrack;
using DaireYonetimAPI.Business.Concrete;
using DaireYonetimAPI.DataAccess;
using DaireYonetimAPI.DataAccess.Abstrack;
using DaireYonetimAPI.DataAccess.Concrete;
using DaireYönetimAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BakiyeSerivceTest.cs
{
    public class BakiyeServiceTest
    {
    

        [Test]
        public void BakiyeTo_ListTo_Fix()
        {
            // Arrange
            var db = new DaireDbContext();
            var bakiyeRepository = new BakiyeRepository(db);
            var manager = new BakiyeManager(db, bakiyeRepository);

            // Act
            var bakiyeList = manager.GetAllBakiye();

            // Assert

            Assert.IsTrue(bakiyeList.Count > 0);

            foreach (var bakiye in bakiyeList)
            {
                Assert.IsNotNull(bakiye);
               
            }
        }

        [Test]
        public void test_notnull_payment()
        {
            // Arrange
            var db = new DaireDbContext();
            var bakiyeRepository = new BakiyeRepository(db);
            var manager = new BakiyeManager(db, bakiyeRepository);
            int ap = 12;

            // Act
            var payments = manager.Payment(ap);

            // Assert
            Assert.IsNotNull(payments);
        }

        [Test]
        public void test_calculatecurrentdebt_notnull_payment()
        {
            // Arrange
            var db = new DaireDbContext();
            var bakiyeRepositoryMock = new Mock<IBakiyeRepository>();
            var bakiyeManager = new BakiyeManager(db, bakiyeRepositoryMock.Object);

            // Act
            var result = bakiyeManager.calculatecurrentdebt(14, 200);

            // Assert
            Assert.IsNull(result);
        }


        [Test]
        public void test_getbakiyes_notnull_borcdurumuistrue()
        {  
            // Arrange
            var db = new DaireDbContext();
            var bakiyeRepository = new BakiyeRepository(db);
            var manager = new BakiyeManager(db, bakiyeRepository);

            // Act
            var GetBakiye = manager.GetBakiyeler(true);

            // Assert
            Assert.IsNotEmpty(GetBakiye);
            Assert.IsNotNull(GetBakiye);
        }
        
        [Test]
        public void test_getbakiyes_notnull_borcdurumuisfalse()
        {
            // Arrange
            var db = new DaireDbContext();
            var bakiyeRepository = new BakiyeRepository(db);
            var manager = new BakiyeManager(db, bakiyeRepository);

            // Act
            var GetBakiye = manager.GetBakiyeler(false);    

            // Assert
            Assert.IsNotEmpty(GetBakiye);
            Assert.IsNotNull(GetBakiye);
        }

        [Test]
        public void test_paymentstatus_notnull_notempty()
        {
            // Arrange
            var db = new DaireDbContext();
            var bakiyeRepository = new BakiyeRepository(db);
            var manager = new BakiyeManager(db, bakiyeRepository);

            // Act
            var GetPaymentStatus = manager.PaymentStatus(200);
           
            // Assert
            Assert.IsNotNull(GetPaymentStatus);
        }

    }

}

   