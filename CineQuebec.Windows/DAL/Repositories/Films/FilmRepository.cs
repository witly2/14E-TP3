using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories.Films
{
    public class FilmRepository : IFilmRepository
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection de films : " + ex.Message, "Erreur");
            }

            return films;
        }

        public List<Projection> GetProjectionsForFilm(ObjectId  filmId)
        {
            var projections = new List<Projection>();

            try
            {
                var projectionsFilm = Builders<Projection>.Filter.Eq(p => p.Film.Id, filmId);
                projections = _databaseGestion.GetProjectionsCollection().Find(projectionsFilm).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection de projection : " + ex.Message, "Erreur");
            }

            return projections;
        }

        public void AddFilm(Film film)
        {
            try
            {
                _filmsCollection.InsertOne(film);
                Console.WriteLine("Congrats AddFilm");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'ajouter le film : " + ex.Message, "Erreur");
            }
        }

        public void UpdateFilm(Film film)
        {
            try
            {
                var filter = Builders<Film>.Filter.Eq(filmBd => filmBd.Id, film.Id);
                var filmUpdate = Builders<Film>.Update
                    .Set(filmBd => filmBd.OriginalTitle, film.OriginalTitle)
                    .Set(filmBd => filmBd.FrenchTitle, film.FrenchTitle)
                    .Set(filmBd => filmBd.Description, film.Description)
                    .Set(filmBd => filmBd.Duration, film.Duration)
                    .Set(filmBd => filmBd.InternationalReleaseDate, film.InternationalReleaseDate)
                    .Set(filmBd => filmBd.Rating, film.Rating);

                _filmsCollection.UpdateOne(filter, filmUpdate);
                Console.WriteLine("Film mis à jour.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de mettre à jour le film: " + ex.Message, "Erreur");
            }
        }
    }
}
