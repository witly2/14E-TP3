﻿using System.IO;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Films;

namespace CineQuebec.Windows.BLL.Services;

public class FilmService : IFilmService
{
    private readonly IFilmRepository _filmRepository;

    public FilmService(IFilmRepository filmRepository)
    {
        _filmRepository = filmRepository;
    }

    public async Task<List<Film>> GetFilms()
    {
        try
        {
            return await _filmRepository.GetFilms();
        }
        catch (Exception ex)
        {
            throw new InvalidDataException(
                "Une erreur s'est produite lors de la récupération des films : " + ex.Message);
        }
    }

    public async Task<Film> AddFilm(Film film)
    {
        try
        {
            return await _filmRepository.AddFilm(film);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Une erreur s'est produite lors de l'ajout du film : " + ex.Message);
        }
    }

    public async Task<Film> UpdateFilm(Film film)
    {
        try
        {
            return await _filmRepository.UpdateFilm(film);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Une erreur s'est produite lors de la récupération du film : " + ex.Message);
        }
    }

    public async Task<List<Projection>> GetProjections(Film film)
    {
        try
        {
            return await _filmRepository.GetProjectionsForFilm(film);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException(
                "Une erreur s'est produite lors de la récupération des projections du film : " + ex.Message);
        }
    }

    public async Task<bool> DeleteFilm(Film film)
    {
        try
        {
            return await _filmRepository.DeleteFilm(film);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Une erreur s'est produite lors de la suppression du film : " + ex.Message);
        }
    }
}