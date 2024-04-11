using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Films;
using MongoDB.Bson;
using Moq;
using System.Windows;

namespace CineQuebec.WindowsTests.FilmTests
{
    [TestClass()]
    public class FilmServiceTests
    {
        private FilmService _filmService;
        private Mock<IFilmRepository> _mockFilmRepository;
        [TestInitialize]
        public void Init()
        {
            _mockFilmRepository = new Mock<IFilmRepository>();
            _filmService = new FilmService(_mockFilmRepository.Object);
        }

        #region GetFilmTests
        [TestMethod()]
        public async Task GetFilms_Success()
        {
            List<Film> expectedFilms = new List<Film>();
            Film film1 = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8);
            Film film2 = new Film("The Dark Knight", "Le Chevalier Noir", "Lorsque la menace connue sous le nom du Joker sème le chaos parmi les habitants de Gotham, Batman doit accepter l'une des plus grandes épreuves psychologiques et physiques de sa capacité à lutter contre l'injustice.", 152, new DateTime(2008, 7, 18), 9);
            expectedFilms.Add(film1);
            expectedFilms.Add(film2);
            _mockFilmRepository.Setup(x => x.GetFilms()).ReturnsAsync(expectedFilms);

            var result = await _filmService.GetFilms();

            Assert.AreEqual(expectedFilms, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task GetFilms_Exception()
        {
            _mockFilmRepository.Setup(x => x.GetFilms()).ThrowsAsync(new Exception("Erreur lors du telechargement"));

            await _filmService.GetFilms();
        }
        #endregion

        #region AddFilmtests
        [TestMethod()]
        public async Task AddFilm_Success()
        {
            Film addedFilm = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            Film expectedFilm = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            _mockFilmRepository.Setup(x => x.AddFilm(It.IsAny<Film>())).ReturnsAsync(addedFilm);

            var result = await _filmService.AddFilm(expectedFilm);

            Assert.AreEqual(addedFilm, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task AddFilm_Exception()
        {
            Film expectedFilm = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            _mockFilmRepository.Setup(x => x.AddFilm(It.IsAny<Film>())).ThrowsAsync(new Exception("Erreur lors de l'ajout"));

            await _filmService.AddFilm(expectedFilm);
        }
        #endregion

        #region UpdateFilmTests
        [TestMethod()]
        public async Task UpdateFilm_Success()
        {
            Film updatedFilm = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            Film expectedFilm = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            _mockFilmRepository.Setup(x => x.UpdateFilm(It.IsAny<Film>())).ReturnsAsync(updatedFilm);

            var result = await _filmService.UpdateFilm(expectedFilm);

            Assert.AreEqual(updatedFilm, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task UpdateFilm_Exception()
        {
            Film expectedFilm = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            _mockFilmRepository.Setup(x => x.AddFilm(It.IsAny<Film>())).ThrowsAsync(new Exception("Erreur lors de l'update"));

            await _filmService.AddFilm(expectedFilm);
        }
        #endregion

        #region GetProjectionsTests
        [TestMethod()]
        public async Task GetProjections_Success()
        {
            Film expectedFilm = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            Salle salle = new Salle(1, 100);
            List<Projection> expectedProjections = new List<Projection>();
            Projection expectedProjection = new Projection(new DateTime(2024, 3, 10, 20, 0, 0), salle, expectedFilm);
            _mockFilmRepository.Setup(x => x.GetProjectionsForFilm(expectedFilm)).ReturnsAsync(expectedProjections);

            var result = await _filmService.GetProjections(expectedFilm);

            Assert.AreEqual(expectedProjections, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task GetProjections_Exception()
        {
            Film expectedFilm = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",
                                    148, new DateTime(2010, 7, 16), 8);
            _mockFilmRepository.Setup(x => x.GetProjectionsForFilm(It.IsAny<Film>())).ThrowsAsync(new Exception("Erreur lors de la recuperation des projections"));

            await _filmService.GetProjections(expectedFilm);
        }
        #endregion
    }
}
