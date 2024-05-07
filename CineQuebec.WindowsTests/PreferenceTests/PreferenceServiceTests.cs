using CineQuebec.Windows.BLL.Services.Preferences;
using CineQuebec.Windows.DAL.Data;
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
        public void AddPreferenceRealisateur_ShouldAddRealisateurPreferenceToList()
        {
            var preferenceService = new PreferenceService(_mockPreferenceRepository);
            var preference = new Preference();
            var realisateur = new Realisateur("Tarentino", "Quentin");

            preferenceService.AddPreferenceRealisateur(preference, realisateur);

            Assert.IsTrue(preference.ListPreferenceRealisateur.Contains(realisateur));
        }
    }
}
