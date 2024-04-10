using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Films;
using Moq;

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
            
        }
    }
}