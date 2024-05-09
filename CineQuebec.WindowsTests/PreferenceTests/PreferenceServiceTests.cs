using CineQuebec.Windows.BLL.Services.Preferences;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Repositories.Preferences;
using ControlzEx.Standard;
using Moq;
using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace CineQuebec.WindowsTests.PreferenceTests
{
    [TestClass]
    public class PreferenceServiceTests
    {
        private Mock<IPreferenceRepository> _mockPreferenceRepository;
        private PreferenceService _preferenceService;
        [TestInitialize]
        public void Init()
        {
            _mockPreferenceRepository = new Mock<IPreferenceRepository>();
            _preferenceService = new PreferenceService(_mockPreferenceRepository.Object );
        }

        #region AddPreferenceRealisateur
        [TestMethod]
        public void AddPreferenceRealisateurLessFive_ShouldAddRealisateurPreferenceToList()
        {
            var preference = new Preference();
            var list = new List<Realisateur>();
            preference.SetListPreferenceRealisateur(list);
            var realisateur = new Realisateur("Tarentino", "Quentin");

            _preferenceService.AddPreferenceRealisateur(preference, realisateur);

            Assert.IsTrue(preference.ListPreferenceRealisateur.Contains(realisateur));
        }

        [TestMethod]
        [ExpectedException(typeof(MaximumPreferenceException))]
        public void AddPreferenceRealisateurMoreThanFive_ShouldThrowException()
        {
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

            _preferenceService.AddPreferenceRealisateur(preference, new Realisateur("Spielberg", "Steven"));
        }

        #endregion

        #region IsAlreadyInList
        [TestMethod]
        [ExpectedException(typeof(PreferenceAlreadyExistsException))]
        public void IsAlreadyInList_ShouldThrowExceptionPreferenceAlreadyExistsException()
        {
            var preference = new Preference();
            var realisateur = new Realisateur("Tarentino", "Quentin");

            _mockPreferenceRepository.Setup(r => r.IsAlreadyInList(It.IsAny<Preference>(), It.IsAny<Realisateur>(), It.IsAny<Expression<Func<Preference, IEnumerable<Realisateur>>>>()))
                .Returns(true);

            _preferenceService.IsAlreadyInList(preference, realisateur, p => p.ListPreferenceRealisateur);
        }

        [TestMethod]
        public void IsNotAlreadyInList_ShouldNotThrowException()
        {
            var preference = new Preference();
            var realisateur = new Realisateur("Tarentino", "Quentin");

            _mockPreferenceRepository.Setup(r => r.IsAlreadyInList(It.IsAny<Preference>(), It.IsAny<Realisateur>(), It.IsAny<Expression<Func<Preference, IEnumerable<Realisateur>>>>()))
                .Returns(false);

            _preferenceService.IsAlreadyInList(preference, realisateur, p => p.ListPreferenceRealisateur);
        }

        #endregion

        #region AddPreferenceActeur
        [TestMethod]
        public void AddPreferenceActeurLessFive_ShouldAddActeurPreferenceToList()
        {
            var preference = new Preference();
            var list = new List<Acteur>();
            preference.SetListPreferenceActeur(list);
            var acteur = new Acteur { Nom = "Jackson", Prenom = "Samuel Lee" };

            _preferenceService.AddPreferenceActeur(preference, acteur);

            Assert.IsTrue(preference.ListPreferenceActeur.Contains(acteur));
        }

        [TestMethod]
        [ExpectedException(typeof(MaximumPreferenceException))]
        public void AddPreferenceActeurMoreThanFive_ShouldThrowException()
        {
            var preference = new Preference();
            var list = new List<Acteur>()
            {
                new Acteur { Nom = "DiCaprio", Prenom = "Leonardo" },
                new Acteur { Nom = "Nolan", Prenom = "Christopher" },
                new Acteur { Nom = "Cotillard", Prenom = "Marion" },
                new Acteur { Nom = "Bale", Prenom = "Christian" },
                new Acteur { Nom = "Ledger", Prenom = "Heath" },
            };
            preference.SetListPreferenceActeur(list);

            _preferenceService.AddPreferenceActeur(preference, new Acteur { Nom = "Freeman", Prenom = "Morgan" });
        }

        #endregion

        #region AddPreferenceCategorie
        [TestMethod]
        public void AddPreferenceCategorieLessThree_ShouldAddCategoriePreferenceToList()
        {
            var preference = new Preference();
            var list = new List<Categorie>();
            preference.SetListPreferenceCategorie(list);
            var categorie = new Categorie { Nom = "Science Fiction" };

            _preferenceService.AddPreferenceCategorie(preference, categorie);

            Assert.IsTrue(preference.ListPreferenceCategorie.Contains(categorie));
        }

        [TestMethod]
        [ExpectedException(typeof(MaximumPreferenceException))]
        public void AddPreferenceActeurMoreThanThree_ShouldThrowException()
        {
            var preference = new Preference();
            var list = new List<Acteur>()
            {
                new Acteur { Nom = "DiCaprio", Prenom = "Leonardo" },
                new Acteur { Nom = "Nolan", Prenom = "Christopher" },
                new Acteur { Nom = "Cotillard", Prenom = "Marion" },
                new Acteur { Nom = "Bale", Prenom = "Christian" },
                new Acteur { Nom = "Ledger", Prenom = "Heath" },
            };
            preference.SetListPreferenceActeur(list);

            _preferenceService.AddPreferenceActeur(preference, new Acteur { Nom = "Freeman", Prenom = "Morgan" });
        }
        #endregion

        #region RemovePreference
        [TestMethod]
        public void RemovePreference_ShouldRemoveRealisateurFromList()
        {
            var preference = new Preference();
            var realisateur = new Realisateur("Tarentino", "Quentin");
            var list = new List<Realisateur>() { realisateur };
            preference.SetListPreferenceRealisateur(list);

            _preferenceService.RemovePreference(preference, realisateur, p => p.ListPreferenceRealisateur);

            Assert.IsFalse(preference.ListPreferenceRealisateur.Contains(realisateur));
        }

        [TestMethod]
        public void RemovePreference_ShouldNotRemoveNonExistingRealisateur()
        {
            var preference = new Preference();
            var realisateur = new Realisateur("Spielberg", "Steven");
            var list = new List<Realisateur>() { new Realisateur("Tarentino", "Quentin") };
            preference.SetListPreferenceRealisateur(list);

            _preferenceService.RemovePreference(preference, realisateur, p => p.ListPreferenceRealisateur);

            Assert.IsTrue(preference.ListPreferenceRealisateur.Contains(list[0]));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemovePreference_ShouldThrowArgumentNullExceptionWhenPreferenceIsNull()
        {
            var realisateur = new Realisateur("Tarentino", "Quentin");

            _preferenceService.RemovePreference(null, realisateur, p => p.ListPreferenceRealisateur);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemovePreference_ShouldThrowArgumentNullExceptionWhenRealisateurIsNull()
        {
            var preference = new Preference();

            _preferenceService.RemovePreference(preference, null, p => p.ListPreferenceRealisateur);
        }
        #endregion

        #region GetPreferenceAbonne
        [TestMethod]
        public void GetPreferenceAbonne_ShouldReturnPreferencesAbonnes()
        {
            var abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            Realisateur realisateur = new Realisateur("Tarentino", "Quentin");
            Categorie categorie = new Categorie { Nom = "Science Fiction" };
            Acteur acteur = new Acteur { Nom = "Jackson", Prenom = "Samuel Lee" };
            var listRealisateur = new List<Realisateur>() { realisateur };
            var listCategorie = new List<Categorie>() { categorie};
            var listActeur = new List<Acteur>() { acteur };
            var expectedPreferences = new Preference();
            _mockPreferenceRepository.Setup(r => r.GetPreferenceAbonne(abonne)).Returns(expectedPreferences);

            var preferences = _preferenceService.GetPreferenceAbonne(abonne);

            Assert.AreEqual(expectedPreferences, preferences);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetPreferenceAbonne_ShouldReturnThrowsException()
        {
            var abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            _mockPreferenceRepository.Setup(r => r.GetPreferenceAbonne(abonne)).Throws<Exception>();

            _preferenceService.GetPreferenceAbonne(abonne);
        }

        [TestMethod]
        public void GetPreferenceAbonne_ShouldReturnNull()
        {
            var abonne = new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = new DateTime(2010, 7, 16) };
            _mockPreferenceRepository.Setup(r => r.GetPreferenceAbonne(abonne)).Returns<Preference>(null);

            var preferences = _preferenceService.GetPreferenceAbonne(abonne);

            Assert.IsNull(preferences);
        }

        #endregion

        #region ListAbonnesWithThisPreference
        [TestMethod]
        public void ListAbonnesWithThisPreference_ShouldReturnAbonnesWithThisPreference()
        {
            var preference = new Preference();
            var realisateur = new Realisateur("Tarentino", "Quentin");
            preference.SetListPreferenceRealisateur(new List<Realisateur> { realisateur });
            var abonneWithThisPreference = new List<Abonne> 
            {
                new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = DateTime.UtcNow },
                new Abonne { Username = "Jane Smith", Email = "jane.smith@example.com", DateAdhesion = DateTime.UtcNow },
            };
            _mockPreferenceRepository.Setup(r => r.GetAbonnesWithThisPreference(realisateur, It.IsAny<Expression<Func<Preference, IEnumerable<Realisateur>>>>())).Returns(abonneWithThisPreference);

            var abonnes = _preferenceService.GetAbonnesWithThisPreference(realisateur, p => p.ListPreferenceRealisateur);

            Assert.AreEqual(abonneWithThisPreference, abonnes);
        }

        [TestMethod]
        public void ListAbonnesWithThisPreference_ShouldReturnNoAbonnesWithThisPreference()
        {
            var realisateur = new Realisateur("Tarentino", "Quentin");
            _mockPreferenceRepository.Setup(r => r.GetAbonnesWithThisPreference(realisateur, It.IsAny<Expression<Func<Preference, IEnumerable<Realisateur>>>>())).Returns(new List<Abonne>());

            var abonnes = _preferenceService.GetAbonnesWithThisPreference(realisateur, p => p.ListPreferenceRealisateur);

            Assert.AreEqual(0, abonnes?.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ListAbonnesWithThisPreference_ShouldThrowException()
        {
            var realisateur = new Realisateur("Spielberg", "Steven");
            _mockPreferenceRepository.Setup(r => r.GetAbonnesWithThisPreference(realisateur, It.IsAny<Expression<Func<Preference, IEnumerable<Realisateur>>>>())).Throws<Exception>();

            _preferenceService.GetAbonnesWithThisPreference(realisateur, p => p.ListPreferenceRealisateur);
        }
        #endregion
    } 
}
