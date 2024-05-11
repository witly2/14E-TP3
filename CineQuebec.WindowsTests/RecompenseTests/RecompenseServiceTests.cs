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

        #region AjouterRecompenseTypeInvitationAvantPremiere
        [TestMethod]
        public async Task AjouterRecompenseTypeInvitationAvantPremiere_ShouldAddRecompense()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            Projection projection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            Recompense recompenseExpected = new Recompense(abonne, TypeRecompense.InvitationAvantPremiere, projection);
            _mockRecompenseRepository.Setup(m => m.AjouterRecompenseAvantPremiere(It.IsAny<Recompense>())).ReturnsAsync(recompenseExpected);

            var result = await _recompenseService.AjouterRecompenseAvantPremiere(recompenseExpected);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async void AjouterRecompenseTypeInvitationAvantPremiere_ShouldReturnThrowException()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            Projection projection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), new Salle(1, 100),
                new Film("Inception", "Inception", "Un voleur qui entre dans les r�ves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8));
            Recompense recompenseExpected = new Recompense(abonne, TypeRecompense.InvitationAvantPremiere, projection);
            _mockRecompenseRepository.Setup(x => x.AjouterRecompenseAvantPremiere(It.IsAny<Recompense>())).ThrowsAsync(new Exception("Erreur lors de l'ajout"));

            await _recompenseService.AjouterRecompenseAvantPremiere(recompenseExpected);

        }
        #endregion

        #region AjouterRecompenseTypeTicketGratuit
        [TestMethod]
        public async Task AjouterRecompenseTypeTicketGratuite_ShouldAddRecompense()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            DateTime dateExpiration = DateTime.Now.AddMonths(6);
            Recompense recompenseExpected = new Recompense(abonne, TypeRecompense.TicketGratuit, dateExpiration);
            _mockRecompenseRepository.Setup(m => m.AjouterRecompenseAvantPremiere(It.IsAny<Recompense>())).ReturnsAsync(recompenseExpected);

            var result = await _recompenseService.AjouterRecompenseAvantPremiere(recompenseExpected);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async void AjouterRecompenseTypeTicketGratuite_ShouldReturnThrowException()
        {
            Abonne abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            DateTime dateExpiration = DateTime.Now.AddMonths(6);
            Recompense recompenseExpected = new Recompense(abonne, TypeRecompense.TicketGratuit, dateExpiration);
            _mockRecompenseRepository.Setup(x => x.AjouterRecompenseAvantPremiere(It.IsAny<Recompense>())).ThrowsAsync(new Exception("Erreur lors de l'ajout"));

            await _recompenseService.AjouterRecompenseAvantPremiere(recompenseExpected);

        }
        #endregion
    }
}
