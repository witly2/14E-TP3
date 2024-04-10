﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Repositories.Abonnés;
using Moq;

namespace CineQuebec.WindowsTests.Abonnes
{

    [TestClass()]
    public class AbonneServiceTest
    {

        private AbonneService _abonneService;
        private Mock<IAbonneRepsitory> _mockAbonneRepository;
        [TestInitialize]
        public void Init()
        {
            _mockAbonneRepository = new Mock<IAbonneRepsitory>();
            _abonneService = new AbonneService(_mockAbonneRepository.Object);
        }

        [TestMethod]
        public async Task GetAbonnesSuccess()
        {
            // Arrange
            List<Abonne> expectedAbonne  = new List<Abonne>()
            {
                new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = DateTime.UtcNow },
                new Abonne { Username = "Jane Smith", Email = "jane.smith@example.com", DateAdhesion = DateTime.UtcNow },
                   
            };

            _mockAbonneRepository.Setup(x=>x.GetAbonnes()).Returns(expectedAbonne);

            //act
            var result = _abonneService.GetAbonnes();

            // Assert
            Assert.IsNotNull(result);

            CollectionAssert.AreEqual(expectedAbonne, result);
        }


        [TestMethod]
        public void GetAbonnesFailure_EmptyListReturned()
        {
            // Arrange
            _mockAbonneRepository.Setup(x => x.GetAbonnes()).Returns(new List<Abonne>());

            // Act
            var result = _abonneService.GetAbonnes();

            // Assert
            Assert.IsNotNull(result); 
            Assert.AreEqual(0, result.Count); 
        }


        //[TestMethod]
        //public void 
    }
}
