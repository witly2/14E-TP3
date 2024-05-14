using System;
using System.Collections.Generic;
using System.IO;
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
                throw new InvalidDataException("Impossible d'obtenir la collection de films : " + ex.Message);
            }
        }

        public async Task<List<Film>> GetFilmsAvecProjectionsFutures()
        {
            try
            {
                var futuresProjectionsFilter = Builders<Projection>.Filter.Gt(p => p.DateHeureDebut, DateTime.Now);
                var filmIdsWithFutureProjections = await _databaseGestion.GetProjectionsCollection().Result
                    .Find(futuresProjectionsFilter)
                    .Project(p => p.Film.Id)
                    .ToListAsync();

                var filmsWithFutureProjections = await _filmsCollection
                    .Find(film => filmIdsWithFutureProjections.Contains(film.Id))
                    .ToListAsync();

                return filmsWithFutureProjections;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir les films avec des projections futures : " + ex.Message);
            }
        }

        public async Task<List<Projection>> GetProjectionsForFilm(Film  film)
        {
            try
            {
                var projectionsFilm = Builders<Projection>.Filter.Eq(p => p.Film.Id, film.Id);
                return await _databaseGestion.GetProjectionsCollection().Result.Find(projectionsFilm).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection de projection : " + ex.Message);
            }
        }

        public async Task<List<Projection>> GetProjectionsFuturesForFilm(Film film)
        {
            try
            {
                var allProjectionsFilmFilter = Builders<Projection>.Filter.Eq(p => p.Film.Id, film.Id);
                var futuresProjectionsFilter = Builders<Projection>.Filter.Gt(p => p.DateHeureDebut, DateTime.Now);

                var combinedFilter = Builders<Projection>.Filter.And(allProjectionsFilmFilter, futuresProjectionsFilter);

                return await _databaseGestion.GetProjectionsCollection().Result.Find(combinedFilter).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection de projection : " + ex.Message);
            }
        }

        public async Task<Film> AddFilm(Film film)
        {
            try
            {
                await _filmsCollection.InsertOneAsync(film);
                return film;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'ajouter le film : " + ex.Message);
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
                return film;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible de mettre à jour le film: " + ex.Message);
            }
        }

        public async Task<bool> DeleteFilm(Film film)
        {
            try
            {
                var deleteResult = await _filmsCollection.DeleteOneAsync(filmBd => filmBd.Id == film.Id);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de supprimer le film : " + ex.Message);
                return false;
            }
        }

        public async Task<List<Realisateur>> GetAllRealisateurs()
        {
            try
            {
                return await _databaseGestion.GetRealisateursCollection().Result.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection des realisateurs : " + ex.Message);
            }
            
        }
        public async Task<List<Acteur>> GetAllActeurs()
        {
            try
            {
                return await _databaseGestion.GetActeursCollection().Result.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection des acteurs : " + ex.Message);
            }
        }
        public async Task<List<Categorie>> GetAllCategories()
        {
            try
            {
                return await _databaseGestion.GetCategoriesCollection().Result.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection des categories : " + ex.Message);
            }
        }
    }
}
