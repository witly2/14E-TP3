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
            _filmsCollection = _databaseGestion.GetFilmsCollection().Result;
        }

        public async Task<List<Film>> GetFilms()
        {
            try
            {
                return await _filmsCollection.Aggregate().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection de films : " + ex.Message, "Erreur");
                return new List<Film>();
            }
        }

        public async Task<List<Projection>> GetProjectionsForFilm(ObjectId  filmId)
        {
            try
            {
                var projectionsFilm = Builders<Projection>.Filter.Eq(p => p.Film.Id, filmId);
                return await _databaseGestion.GetProjectionsCollection().Result.Find(projectionsFilm).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection de projection : " + ex.Message, "Erreur");
                return new List<Projection>();
            }
        }

        public async Task<Film> AddFilm(Film film)
        {
            try
            {
                await _filmsCollection.InsertOneAsync(film);
                Console.WriteLine("Congrats AddFilm");
                return film;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'ajouter le film : " + ex.Message, "Erreur");
                return null;
            }
        }

        public async Task<Film> UpdateFilm(Film film)
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

                await _filmsCollection.UpdateOneAsync(filter, filmUpdate);
                Console.WriteLine("Film mis à jour.");
                return film;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de mettre à jour le film: " + ex.Message, "Erreur");
                return null;
            }
        }
    }
}
