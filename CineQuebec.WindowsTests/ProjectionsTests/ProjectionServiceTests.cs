using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Projections;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System.Windows;

namespace CineQuebec.WindowsTests.FilmTests
{
    [TestClass()]
    public class ProjectionServiceTests
    {
        private Mock<DatabaseGestion> _mockDatabaseGestion;
        private ProjectionService _projectionService;
        private Mock<IProjectionRepository> _mockProjectionRepository;
        [TestInitialize]
        public void Init()
        {
            _mockDatabaseGestion = new Mock<DatabaseGestion>();
            _mockProjectionRepository = new Mock<IProjectionRepository>();
            _projectionService = new ProjectionService(_mockProjectionRepository.Object);
        }

        [TestMethod()]
        public async Task AddProjection_Success()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100), 
                new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.AddProjection(It.IsAny<Projection>())).ReturnsAsync(expectedProjection);

            var result = await _projectionService.AddProjection(expectedProjection);

            Assert.AreEqual(expectedProjection, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task AddProjection_Exception()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.AddProjection(It.IsAny<Projection>())).ThrowsAsync(new Exception("Erreur lors de l'ajout"));

            await _projectionService.AddProjection(expectedProjection);
        }

        [TestMethod()]
        public async Task DeleteProjection_Success()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.DeleteProjection(expectedProjection)).ReturnsAsync(true);

            var result = await _projectionService.DeleteProjection(expectedProjection);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task DeleteProjection_Fail()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.DeleteProjection(expectedProjection)).ThrowsAsync(new Exception("Erreur lors de la suppression.")); ;

            await _projectionService.DeleteProjection(expectedProjection);
        }

        [TestMethod()]
        public async Task GetProjections_Success()
        {
            Film film = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            var projectionsCollectionMock = new Mock<IMongoCollection<Projection>>();
            var expectedProjections = new List<Projection>();
            var projection1 = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            var projection2 = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            expectedProjections.Add(projection1);
            expectedProjections.Add(projection2);
            //projectionsCollectionMock.Setup(x => x.Find(It.IsAny<FilterDefinition<Projection>>()).ToList()).Returns(expectedProjections);
            _mockDatabaseGestion.Setup(x => x.GetProjectionsCollection()).ReturnsAsync(projectionsCollectionMock.Object);
            _mockProjectionRepository.Setup(x => x.GetProjectionsForFilm(film));
            
            var result = await _projectionService.GetProjections(film);

            Assert.AreEqual(expectedProjections, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task GetProjections_Exception()
        {
            
        }

        [TestMethod()]
        public async Task UpdateProjection_Success()
        {
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            _mockProjectionRepository.Setup(x => x.UpdateProjection(expectedProjection)).ReturnsAsync(true);

            var result = await _projectionService.UpdateProjection(expectedProjection);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task UpdateProjection_Exception()
        {
            
        }
    }
}
