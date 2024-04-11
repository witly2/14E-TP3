using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Films;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.WindowsTests.FilmTests
{
    [TestClass()]
    public class FilmRepositoryTests
    {
        private Mock<DatabaseGestion> _mockDatabaseGestion;
        private FilmRepository _filmRepository;

        [TestInitialize]
        public void Init()
        {
            _mockDatabaseGestion = new Mock<DatabaseGestion>();
            _filmRepository = new FilmRepository(_mockDatabaseGestion.Object);
        }

        [TestMethod()]
        public async Task GetFilms_Success()
        {
            var filmsCollectionMock = new Mock<IMongoCollection<Film>>();
            List<Film> expectedFilms = new List<Film>();
            Film film1 = new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8);
            Film film2 = new Film("The Dark Knight", "Le Chevalier Noir", "Lorsque la menace connue sous le nom du Joker sème le chaos parmi les habitants de Gotham, Batman doit accepter l'une des plus grandes épreuves psychologiques et physiques de sa capacité à lutter contre l'injustice.", 152, new DateTime(2008, 7, 18), 9);
            expectedFilms.Add(film1);
            expectedFilms.Add(film2);
            //filmsCollectionMock.Setup(x => x.Aggregate().ToListAsync()).ReturnsAsync(expectedFilms);
            _mockDatabaseGestion.Setup(x => x.GetFilmsCollection()).ReturnsAsync(filmsCollectionMock.Object);


            var result = await _filmRepository.GetFilms();

            Assert.AreEqual(expectedFilms.Count, result.Count);
        }
    }
}
