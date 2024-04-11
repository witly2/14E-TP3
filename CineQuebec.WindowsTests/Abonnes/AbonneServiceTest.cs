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

        #region GetAbonnesTests
        [TestMethod]
        public async Task GetAbonnesSuccess()
        {
            List<Abonne> expectedAbonne  = new List<Abonne>()
            {
                new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) },
                new Abonne { Username = "Jane Smith", Email = "jane.smith@example.com", DateAdhesion = new DateTime(2010, 7, 16) },
                   
            };
            _mockAbonneRepository.Setup(x=>x.GetAbonnes()).Returns(expectedAbonne);


            var result = _abonneService.GetAbonnes();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedAbonne, result);
        }


        [TestMethod]
        public void GetAbonnesFailure_EmptyListReturned()
        {
            _mockAbonneRepository.Setup(x => x.GetAbonnes()).Returns(new List<Abonne>());

            var result = _abonneService.GetAbonnes();

            Assert.IsNotNull(result); 
            Assert.AreEqual(0, result.Count); 
        }
        #endregion

        #region AddAbonneTests
        [TestMethod]
        public async Task AddAbonneSuccess()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "joh.doe@example.com", DateAdhesion = DateTime.UtcNow };
            _mockAbonneRepository.Setup(x => x.AddAbonne(It.IsAny<Abonne>()));

            await _abonneService.AddAbonne(abonne);

            _mockAbonneRepository.Verify(x => x.AddAbonne(abonne), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(EmailExisteExeption))]
        public async Task AddAbonne_EmailExistException()
        {
            Abonne expected_abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = DateTime.UtcNow };
            _mockAbonneRepository.Setup(x => x.AddAbonne(It.IsAny<Abonne>())).ThrowsAsync(new EmailExisteExeption("Un abonnée existe dejà avec ce courriel"));

            await _abonneService.AddAbonne(expected_abonne);
        }
        #endregion

        #region GetAbonneByEmail
        [TestMethod]
        public async Task GetAbonneByEmail()
        {
            string email = "john.doe@example.com";
            Abonne expectedAbonne = new Abonne { Username = "John Doe", Email = email, DateAdhesion = new DateTime(2010, 7, 16) };
            _mockAbonneRepository.Setup(x => x.GetByEmail(email)).ReturnsAsync(expectedAbonne);

            var result = await _abonneService.GetAbonneByEmail(email);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAbonne, result);

        }

        [TestMethod]
        [ExpectedException(typeof(EmailNotExiseException))]
        public async Task GetAbonneByEmail_EmailNotFound()
        {
            string email = "john.doe@example.com";

            _mockAbonneRepository.Setup(x => x.GetByEmail(email)).ThrowsAsync(new EmailNotExiseException("Auccun abonné ne possède ce courriel"));

            var result = await _abonneService.GetAbonneByEmail(email);
        }
        #endregion
    }
}
