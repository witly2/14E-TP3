using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Films;
using Moq;
using System.Windows;

namespace CineQuebec.WindowsTests
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

        [TestMethod()]
        public async Task GetFilms_Success()
        {
            List<Film> expectedFilms = new List<Film>();
            Film film1 = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8);
            expectedFilms.Add(film1);

        }
    }
}
