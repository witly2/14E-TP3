﻿using System.IO;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL.Repositories.Films;

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

    public async Task<List<Projection>> GetProjectionsForFilm(Film film)
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
}