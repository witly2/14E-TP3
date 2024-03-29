using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class FilmRepository: IFilmRepository
    {
        private readonly DatabaseGestion _databaseGestion;
        private readonly IMongoCollection<Film> _filmsCollection;

        public FilmRepository(DatabaseGestion databaseGestion)
        {
            _databaseGestion = databaseGestion;
            _filmsCollection = _databaseGestion.GetFilmsCollection();
        }

        public List<Film> GetFilms()
        {
            var films = new List<Film>();

            try
            {
                films = _filmsCollection.Aggregate().ToList();
                Console.WriteLine("films ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection de films " + ex.Message, "Erreur");
            }

            return films;
        }
    }
}
