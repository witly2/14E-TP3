using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Repositories.Abonnés;
using Moq;

namespace CineQuebec.WindowsTests.Abonnes;

[TestClass]
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
        var expectedAbonne = new List<Abonne>
        {
            new() { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) },
            new()
            {
                Username = "Jane Smith", Email = "jane.smith@example.com", DateAdhesion = new DateTime(2010, 7, 16)
            }
        };
        _mockAbonneRepository.Setup(x => x.GetAbonnes()).Returns(expectedAbonne);


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
        var abonne = new Abonne
            { Username = "John Doe", Email = "joh.doe@example.com", DateAdhesion = DateTime.UtcNow };
        _mockAbonneRepository.Setup(x => x.AddAbonne(It.IsAny<Abonne>()));

        await _abonneService.AddAbonne(abonne);

        _mockAbonneRepository.Verify(x => x.AddAbonne(abonne), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(EmailExisteExeption))]
    public async Task AddAbonne_EmailExistException()
    {
        var expected_abonne = new Abonne
            { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = DateTime.UtcNow };
        _mockAbonneRepository.Setup(x => x.AddAbonne(It.IsAny<Abonne>()))
            .ThrowsAsync(new EmailExisteExeption("Un abonnée existe dejà avec ce courriel"));

        await _abonneService.AddAbonne(expected_abonne);
    }

    #endregion

    #region GetAbonneByEmail

    [TestMethod]
    public async Task GetAbonneByEmail()
    {
        var email = "john.doe@example.com";
        var expectedAbonne = new Abonne
            { Username = "John Doe", Email = email, DateAdhesion = new DateTime(2010, 7, 16) };
        _mockAbonneRepository.Setup(x => x.GetByEmail(email)).ReturnsAsync(expectedAbonne);

        var result = await _abonneService.GetAbonneByEmail(email);

        Assert.IsNotNull(result);
        Assert.AreEqual(expectedAbonne, result);
    }

    [TestMethod]
    [ExpectedException(typeof(EmailNotExiseException))]
    public async Task GetAbonneByEmail_EmailNotFound()
    {
        var email = "john.doe@example.com";

        _mockAbonneRepository.Setup(x => x.GetByEmail(email))
            .ThrowsAsync(new EmailNotExiseException("Auccun abonné ne possède ce courriel"));

        var result = await _abonneService.GetAbonneByEmail(email);
    }

    #endregion
}