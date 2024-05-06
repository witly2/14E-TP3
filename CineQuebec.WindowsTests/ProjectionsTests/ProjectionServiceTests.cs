using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Projections;
using Moq;

namespace CineQuebec.WindowsTests.FilmTests;

[TestClass]
public class ProjectionServiceTests
{
    private Mock<DatabaseGestion> _mockDatabaseGestion;
    private Mock<IProjectionRepository> _mockProjectionRepository;
    private ProjectionService _projectionService;

    [TestInitialize]
    public void Init()
    {
        _mockDatabaseGestion = new Mock<DatabaseGestion>();
        _mockProjectionRepository = new Mock<IProjectionRepository>();
        _projectionService = new ProjectionService(_mockProjectionRepository.Object);
    }

    #region AddProjectionTests

    [TestMethod]
    public async Task AddProjection_Success()
    {
        var expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
            new Film("Inception", "Inception",
                "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148,
                new DateTime(2010, 7, 16), 8));
        _mockProjectionRepository.Setup(x => x.AddProjection(It.IsAny<Projection>())).ReturnsAsync(expectedProjection);

        var result = await _projectionService.AddProjection(expectedProjection);

        Assert.AreEqual(expectedProjection, result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public async Task AddProjection_Exception()
    {
        var expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
            new Film("Inception", "Inception",
                "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148,
                new DateTime(2010, 7, 16), 8));
        _mockProjectionRepository.Setup(x => x.AddProjection(It.IsAny<Projection>()))
            .ThrowsAsync(new Exception("Erreur lors de l'ajout"));

        await _projectionService.AddProjection(expectedProjection);
    }

    #endregion

    #region DeleteProjectionTests

    [TestMethod]
    public async Task DeleteProjection_Success()
    {
        var expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
            new Film("Inception", "Inception",
                "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148,
                new DateTime(2010, 7, 16), 8));
        _mockProjectionRepository.Setup(x => x.DeleteProjection(expectedProjection)).ReturnsAsync(true);

        var result = await _projectionService.DeleteProjection(expectedProjection);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public async Task DeleteProjection_Fail()
    {
        var expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
            new Film("Inception", "Inception",
                "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148,
                new DateTime(2010, 7, 16), 8));
        _mockProjectionRepository.Setup(x => x.DeleteProjection(expectedProjection))
            .ThrowsAsync(new Exception("Erreur lors de la suppression."));

        await _projectionService.DeleteProjection(expectedProjection);
    }

    #endregion

    #region GetProjectionstests

    [TestMethod]
    public async Task GetProjections_Success()
    {
        var film = new Film("Inception", "Inception",
            "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
            148, new DateTime(2010, 7, 16), 8);
        var expectedProjections = new List<Projection>();
        var projection1 = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
            new Film("Inception", "Inception",
                "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148,
                new DateTime(2010, 7, 16), 8));
        var projection2 = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
            new Film("Inception", "Inception",
                "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148,
                new DateTime(2010, 7, 16), 8));
        expectedProjections.Add(projection1);
        expectedProjections.Add(projection2);
        _mockProjectionRepository.Setup(x => x.GetProjectionsForFilm(film)).ReturnsAsync(expectedProjections);

        var result = await _projectionService.GetProjections(film);

        CollectionAssert.AreEqual(expectedProjections, result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public async Task GetProjections_Exception()
    {
        var film = new Film("Inception", "Inception",
            "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
            148, new DateTime(2010, 7, 16), 8);
        _mockProjectionRepository.Setup(x => x.GetProjectionsForFilm(film))
            .ThrowsAsync(new Exception("Erreur lors de la recherche des projections."));

        await _projectionService.GetProjections(film);
    }

    #endregion

    #region UpdateProjectionTests

    [TestMethod]
    public async Task UpdateProjection_Success()
    {
        var expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
            new Film("Inception", "Inception",
                "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148,
                new DateTime(2010, 7, 16), 8));
        _mockProjectionRepository.Setup(x => x.UpdateProjection(expectedProjection)).ReturnsAsync(true);

        var result = await _projectionService.UpdateProjection(expectedProjection);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public async Task UpdateProjection_Exception()
    {
        var expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
            new Film("Inception", "Inception",
                "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148,
                new DateTime(2010, 7, 16), 8));
        _mockProjectionRepository.Setup(x => x.UpdateProjection(expectedProjection))
            .ThrowsAsync(new Exception("Erreur lors de la mise à jour de la projection."));

        await _projectionService.UpdateProjection(expectedProjection);
    }

    #endregion

    #region GetSallesTests

    [TestMethod]
    public async Task GetSalles_Success()
    {
        var expectedSalles = new List<Salle>();
        var salle1 = new Salle(1, 100);
        var salle2 = new Salle(2, 150);
        expectedSalles.Add(salle1);
        expectedSalles.Add(salle2);
        _mockProjectionRepository.Setup(x => x.GetSalles()).ReturnsAsync(expectedSalles);

        var result = await _projectionService.GetSalles();

        CollectionAssert.AreEqual(expectedSalles, result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public async Task GetSalles_Exception()
    {
        _mockProjectionRepository.Setup(x => x.GetSalles())
            .ThrowsAsync(new Exception("Erreur lors de la recherche des salles."));

        await _projectionService.GetSalles();
    }

    #endregion

    #region EstSalleDisponibleThisDay

    [TestMethod]
    public async Task EstSalleDisponibleThisDay_Success()
    {
        var salle1 = new Salle(1, 100);
        var day = new DateTime(2024, 3, 10);
        _mockProjectionRepository.Setup(x => x.GetProjectionsForSalle(salle1, day))
            .ReturnsAsync(new List<Projection>());

        var result = await _projectionService.estSalleDisponibleThisDay(salle1, day);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task EstSalleDisponibleThisDay_NoDisponible()
    {
        var salle1 = new Salle(1, 100);
        var day = new DateTime(2024, 3, 10);
        var projection1 = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
            new Film("Inception", "Inception",
                "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148,
                new DateTime(2010, 7, 16), 8));
        var projections = new List<Projection>();
        projections.Add(projection1);
        _mockProjectionRepository.Setup(x => x.GetProjectionsForSalle(salle1, day)).ReturnsAsync(projections);

        var result = await _projectionService.estSalleDisponibleThisDay(salle1, day);

        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public async Task EstSalleDisponibleThisDay_Exception()
    {
        var salle1 = new Salle(1, 100);
        var day = new DateTime(2024, 3, 10);
        _mockProjectionRepository.Setup(x => x.GetProjectionsForSalle(salle1, day))
            .ThrowsAsync(new Exception("Erreur lors de la recherche des projections ce jour."));

        var result = await _projectionService.estSalleDisponibleThisDay(salle1, day);
    }

    #endregion
}