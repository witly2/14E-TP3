using CineQuebec.Windows.BLL.Services.Recompenses;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Recompenses;
using Moq;

namespace CineQuebec.WindowsTests.RecompenseTests
{
    [TestClass]
    public class RecompenseServiceTests
    {
        private Mock<IRecompenseRepository> _mockRecompenseRepository;
        private IRecompenseService _recompenseService;

        [TestInitialize]
        public void Init()
        {
            _mockRecompenseRepository = new Mock<IRecompenseRepository>();
            _recompenseService = new RecompenseService(_mockRecompenseRepository.Object);
        }

        #region GetAllRecompenses
        [TestMethod]
        public async Task GetAllRecompenses_ShouldReturnAllRecompenses()
        {
            List<Recompense> recompenses = new List<Recompense>();
            List<Abonne> listAbonnes = new List<Abonne>();
            Projection projection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
               new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            Recompense recompense1 = new Recompense(listAbonnes, TypeRecompense.Ticket, projection, 3);
            Recompense recompense2 = new Recompense(listAbonnes, TypeRecompense.Ticket, projection, 3);
            recompenses.Add(recompense1);
            recompenses.Add(recompense2);
            _mockRecompenseRepository.Setup(x => x.GetAllRecompenses()).ReturnsAsync(recompenses);

            var result = await _recompenseService.GetAllRecompenses();

            Assert.AreEqual(recompenses.Count, result.Count);
            Assert.AreEqual(recompenses, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task GetAllRecompense_ShouldThrowException()
        {
            _mockRecompenseRepository.Setup(x => x.GetAllRecompenses()).ThrowsAsync(new InvalidDataException("Erreur lors du retour des récompenses."));

            await _recompenseService.GetAllRecompenses();
        }

        #endregion

        #region AjouterRecompense
        [TestMethod]
        public async Task AjouterRecompense_ShouldAddRecompense()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            List<Abonne> listAbonnes = new List<Abonne>();
            listAbonnes.Add(abonne);
            Projection projection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            Recompense recompenseExpected = new Recompense(listAbonnes, TypeRecompense.Invitation, projection, 1);
            _mockRecompenseRepository.Setup(x => x.AjouterRecompense(It.IsAny<Recompense>())).ReturnsAsync(recompenseExpected);

            var result = await _recompenseService.AjouterRecompense(recompenseExpected);

            Assert.IsNotNull(result);
            Assert.AreEqual(recompenseExpected, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task AjouterRecompense_ShouldReturnThrowException()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            List<Abonne> listAbonnes = new List<Abonne>();
            listAbonnes.Add(abonne);
            Projection projection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            Recompense recompenseExpected = new Recompense(listAbonnes, TypeRecompense.Invitation, projection, 1);
            _mockRecompenseRepository.Setup(x => x.AjouterRecompense(It.IsAny<Recompense>())).ThrowsAsync(new InvalidDataException("Erreur lors de l'ajout"));

            await _recompenseService.AjouterRecompense(recompenseExpected);
        }
        #endregion

        #region GetCountRecompenseAbonne
        [TestMethod]
        public async Task GetCountRecompenseAbonne_ShouldReturnCountRecompense()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            List<Recompense> recompenses = new List<Recompense>();
            List<Abonne> listAbonnes = new List<Abonne>();
            listAbonnes.Add(abonne);
            List<Abonne> listAbonnes2 = new List<Abonne>();
            listAbonnes2.Add(abonne);
            Projection projection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
               new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            Recompense recompense1 = new Recompense(listAbonnes, TypeRecompense.Ticket, projection, 3);
            Recompense recompense2 =  new Recompense(listAbonnes2, TypeRecompense.Ticket, projection, 3);
            recompenses.Add(recompense1);
            recompenses.Add(recompense2);
            _mockRecompenseRepository.Setup(x => x.GetRecompenseAbonne(It.IsAny<Abonne>())).ReturnsAsync(recompenses);

            var result = await _recompenseService.GetCountRecompenseAbonne(abonne);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public async Task GetCountRecompenseAbonne_ShouldReturnZeroWhenNoRecompense()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            List<Recompense> recompenses = new List<Recompense>();
            _mockRecompenseRepository.Setup(x => x.GetRecompenseAbonne(It.IsAny<Abonne>())).ReturnsAsync(recompenses);

            var result = await _recompenseService.GetCountRecompenseAbonne(abonne);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task GetCountRecompenseAbonne_ShouldThrowException()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            _mockRecompenseRepository.Setup(x => x.GetRecompenseAbonne(It.IsAny<Abonne>())).ThrowsAsync(new InvalidDataException("Erreur lors du retour des recompenses."));

            await _recompenseService.GetCountRecompenseAbonne(abonne);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetCountRecompenseAbonne_ShouldThrowExceptionAbonneNull()
        {
            await _recompenseService.GetCountRecompenseAbonne(null);
        }
        #endregion

        #region GetCountPlaceRestantRecompense
        [TestMethod]
        public async Task GetCountPlaceRestantRecompense_ShouldReturnNumberPlaceRestante()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            List<Abonne> listAbonnes = new List<Abonne>();
            listAbonnes.Add(abonne);
            Projection projection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
               new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            Recompense recompense = new Recompense(listAbonnes, TypeRecompense.Ticket, projection, 3);

            var result = await _recompenseService.GetCountPlaceRestante(recompense);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetCountPlaceRestantRecompense_ShouldReturnThrowException()
        {
            await _recompenseService.GetCountPlaceRestante(null);
        }
        #endregion
    }
}
