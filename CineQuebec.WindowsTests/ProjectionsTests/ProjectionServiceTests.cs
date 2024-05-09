using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Projections;
using Moq;

namespace CineQuebec.WindowsTests.FilmTests;

[TestClass]
public class ProjectionServiceTests
{
      private ProjectionService _projectionService;
        private Mock<IProjectionRepository> _mockProjectionRepository;
        [TestInitialize]
        public void Init()
        {
            _mockProjectionRepository = new Mock<IProjectionRepository>();
            _projectionService = new ProjectionService(_mockProjectionRepository.Object);
        }

        #region AddProjectionTests
        [TestMethod()]
        public async Task AddProjection_Success()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100), 
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.AddProjection(It.IsAny<Projection>())).ReturnsAsync(expectedProjection);

            var result = await _projectionService.AddProjection(expectedProjection);

            Assert.AreEqual(expectedProjection, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task AddProjection_Exception()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.AddProjection(It.IsAny<Projection>())).ThrowsAsync(new Exception("Erreur lors de l'ajout"));

            await _projectionService.AddProjection(expectedProjection);
        }
        #endregion

        #region DeleteProjectionTests
        [TestMethod()]
        public async Task DeleteProjection_Success()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.DeleteProjection(expectedProjection)).ReturnsAsync(true);

            var result = await _projectionService.DeleteProjection(expectedProjection);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task DeleteProjection_Fail()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.DeleteProjection(expectedProjection)).ThrowsAsync(new Exception("Erreur lors de la suppression."));

            await _projectionService.DeleteProjection(expectedProjection);
        }
        #endregion

        #region GetProjectionstests
        [TestMethod()]
        public async Task GetProjections_Success()
        {
            Film film = new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            var expectedProjections = new List<Projection>();
            var projection1 = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            var projection2 = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            expectedProjections.Add(projection1);
            expectedProjections.Add(projection2);
            _mockProjectionRepository.Setup(x => x.GetProjectionsForFilm(film)).ReturnsAsync(expectedProjections);
            
            var result = await _projectionService.GetProjections(film);

            CollectionAssert.AreEqual(expectedProjections, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task GetProjections_Exception()
        {
            Film film = new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            _mockProjectionRepository.Setup(x => x.GetProjectionsForFilm(film)).ThrowsAsync(new Exception("Erreur lors de la recherche des projections."));

            await _projectionService.GetProjections(film);
        }
        #endregion

        #region UpdateProjectionTests
        [TestMethod()]
        public async Task UpdateProjection_Success()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.UpdateProjection(expectedProjection)).ReturnsAsync(true);

            var result = await _projectionService.UpdateProjection(expectedProjection);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task UpdateProjection_Exception()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.UpdateProjection(expectedProjection)).ThrowsAsync(new Exception("Erreur lors de la mise � jour de la projection."));

            await _projectionService.UpdateProjection(expectedProjection);
        }
        #endregion

        #region GetSallesTests
        [TestMethod()]
        public async Task GetSalles_Success()
        {
            List<Salle> expectedSalles = new List<Salle>();
            var salle1 = new Salle(1, 100);
            var salle2 = new Salle(2, 150);
            expectedSalles.Add(salle1);
            expectedSalles.Add(salle2);
            _mockProjectionRepository.Setup(x => x.GetSalles()).ReturnsAsync(expectedSalles);

            var result = await _projectionService.GetSalles();

            CollectionAssert.AreEqual(expectedSalles, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task GetSalles_Exception()
        {
            _mockProjectionRepository.Setup(x => x.GetSalles()).ThrowsAsync(new Exception("Erreur lors de la recherche des salles."));

            await _projectionService.GetSalles();
        }
        #endregion

        #region GetSallesDisponibles
        [TestMethod()]
        public async Task GetSallesDisponibleForProjection_Success()
        {
            var film = new Film("Inception", "Inception", "Description", 148, DateTime.Now, 8);
            var debutProjectionSouhaite = new DateTime(2024, 3, 10, 20, 0, 0);
            var sallesDisponiblesAttendues = new List<Salle> { new Salle(1, 100), new Salle(2, 150) };
            _mockProjectionRepository.Setup(x => x.GetSallesDisponibleForProjection(film, debutProjectionSouhaite)).ReturnsAsync(sallesDisponiblesAttendues);

            var result = await _projectionService.GetSallesDisponibles(film, debutProjectionSouhaite);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(sallesDisponiblesAttendues, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task GetSallesDisponibleForProjection_Exception()
        {
            var film = new Film("Inception", "Inception", "Description", 148, DateTime.Now, 8);
            var debutProjectionSouhaite = new DateTime(2024, 3, 10, 20, 0, 0);
            _mockProjectionRepository.Setup(x => x.GetSallesDisponibleForProjection(film, debutProjectionSouhaite)).ThrowsAsync(new InvalidDataException("Erreur lors de la récupération des salles"));

            await _projectionService.GetSallesDisponibles(film, debutProjectionSouhaite);
        }
        #endregion
    
}
