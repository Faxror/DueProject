
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


        private readonly DaireDbContext db;

        private BakiyeRepository bakiyeRepository;

        private BakiyeManager manager;

        [Test]
        public void Bakiye_Returns_NotNull()
        {
            // Arrange
            
            manager = new BakiyeManager(db, bakiyeRepository);

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
        public void Payments_Returns_NotNull()
        {
            // Arrange
            
             manager = new BakiyeManager(db, bakiyeRepository);
            int ap = 12;

            // Act
            var payments = manager.Payment(ap);

            // Assert
            Assert.IsNotNull(payments);
        }

        [Test]
        public void CalculateCurrentDebt_Returns_Null()
        {
            // Arrange
            var db = new DaireDbContext();
            var bakiyeRepositoryMock = new Mock<IBakiyeRepository>();
            var bakiyeManager = new BakiyeManager(db, bakiyeRepositoryMock.Object);

            // Act
            var result = bakiyeManager.CalculateCurrentDebt(14, 200);

            // Assert
            Assert.IsNull(result);
        }


        [Test]
        public void GetBakiyes_Returns_True_Notnull_NotEmpty()
        {  
            // Arrange
           
             manager = new BakiyeManager(db, bakiyeRepository);

            // Act
            var GetBakiye = manager.GetBakiyeler(true);

            // Assert
            Assert.IsNotEmpty(GetBakiye);
            Assert.IsNotNull(GetBakiye);
        }
        
        [Test]
        public void GetBakiyes_Returns_False_Notnull_NotEmpty()
        {
            // Arrange
           
             manager = new BakiyeManager(db, bakiyeRepository);

            // Act
            var GetBakiye = manager.GetBakiyeler(false);    

            // Assert
            Assert.IsNotEmpty(GetBakiye);
            Assert.IsNotNull(GetBakiye);
        }

        [Test]
        public void paymentstatus_returns_notnull()
        {
            // Arrange
           
             manager = new BakiyeManager(db, bakiyeRepository);

            // Act
            var GetPaymentStatus = manager.PaymentStatus(200);
           
            // Assert
            Assert.IsNotNull(GetPaymentStatus);
        }

    }

}

   