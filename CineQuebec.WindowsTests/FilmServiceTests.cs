using CineQuebec.Windows.BLL.Services;

namespace CineQuebec.WindowsTests
{
    [TestClass()]
    public class FilmServiceTests
    {
        private FilmService _filmService;
        [TestInitialize]
        public void Init()
        {
            _filmService = new FilmService();
        }
    }
}