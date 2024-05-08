using CineQuebec.Windows.BLL.Services.Preferences;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Repositories.Preferences;
using Moq;
using System.ComponentModel.Design;

namespace CineQuebec.WindowsTests.PreferenceTests
{
    [TestClass]
    public class PreferenceServiceTests
    {
        private readonly IPreferenceRepository _mockPreferenceRepository = new Mock<IPreferenceRepository>().Object;

        [TestMethod]
        public void AddPreferenceRealisateurLessFive_ShouldAddRealisateurPreferenceToList()
        {
            var preferenceService = new PreferenceService(_mockPreferenceRepository);
            var preference = new Preference();
            var realisateur = new Realisateur("Tarentino", "Quentin");

            preferenceService.AddPreferenceRealisateur(preference, realisateur);

            Assert.IsTrue(preference.ListPreferenceRealisateur.Contains(realisateur));
        }

        [TestMethod]
        [ExpectedException(typeof(MaximumPreferenceException))]
        public void AddPreferenceRealisateurMoreThanFive_ShouldThrowException()
        {
            var preferenceService = new PreferenceService(_mockPreferenceRepository);
            var preference = new Preference();
            var list = new List<Realisateur>()
            {
                new Realisateur("Nolan", "Christopher"),
                new Realisateur("Chazelle", "Damien"),
                new Realisateur("Villeneuse", "Denis"),
                new Realisateur("Tarantino", "Quentin"),
                new Realisateur("Ford Coppola", "Francis")
            };
            preference.SetListPreferenceRealisateur(list);

            preferenceService.AddPreferenceRealisateur(preference, new Realisateur("Spielberg", "Steven"));
        }

        [TestMethod]
        [ExpectedException(typeof(PreferenceAlreadyExistsException))]
        public void AddExistingPreferenceRealisateur_ShouldThrowException()
        {
            var preferenceService = new PreferenceService(_mockPreferenceRepository);
            var preference = new Preference();
            var realisateur = new Realisateur("Tarentino", "Quentin");

            preferenceService.AddPreferenceRealisateur(preference, realisateur);
            preferenceService.AddPreferenceRealisateur(preference, realisateur);
        }
    }
}
