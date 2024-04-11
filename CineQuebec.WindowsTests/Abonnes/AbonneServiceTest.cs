using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
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
                new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) },
                new Abonne { Username = "Jane Smith", Email = "jane.smith@example.com", DateAdhesion = new DateTime(2010, 7, 16) },
                   
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


        [TestMethod]
        public async Task AddAbonneSuccess()
        {
            // Arrange
            Abonne abonne = new Abonne { Username = "John Doe", Email = "joh.doe@example.com", DateAdhesion = DateTime.UtcNow };
            _mockAbonneRepository.Setup(x => x.AddAbonne(It.IsAny<Abonne>()));

            // Act
            await _abonneService.AddAbonne(abonne);

            // Assert
            _mockAbonneRepository.Verify(x => x.AddAbonne(abonne), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(EmailExisteExeption))]
        public async Task AddAbonne_EmailExistException()
        {
            // Arrange
            Abonne expected_abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = DateTime.UtcNow };
            _mockAbonneRepository.Setup(x => x.AddAbonne(It.IsAny<Abonne>())).ThrowsAsync(new EmailExisteExeption("Un abonnée existe dejà avec ce courriel"));

            // Act
            await _abonneService.AddAbonne(expected_abonne);
        }

        [TestMethod]
        public async Task GetByEmail()
        {
            // Arrange
            string email = "john.doe@example.com";
            Abonne expectedAbonne = new Abonne { Username = "John Doe", Email = email, DateAdhesion = new DateTime(2010, 7, 16) };

            _mockAbonneRepository.Setup(x => x.GetByEmail(email)).ReturnsAsync(expectedAbonne);

            // Act
            var result = await _abonneService.GetAbonneByEmail(email);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAbonne, result);

        }

        [TestMethod]
        [ExpectedException(typeof(EmailNotExiseException))]
        public async Task GetByEmail_EmailNotFound()
        {
            // Arrange
            string email = "john.doe@example.com";

            _mockAbonneRepository.Setup(x => x.GetByEmail(email)).ThrowsAsync(new EmailNotExiseException("Auccun abonné ne possède ce courriel"));

            // Act
            var result = await _abonneService.GetAbonneByEmail(email);

            
        }

    }
}
