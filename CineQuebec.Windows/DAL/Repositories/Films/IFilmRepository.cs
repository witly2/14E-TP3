﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Repositories.Films
{
    public interface IFilmRepository
    {
        Task<List<Film>> GetFilms();
        Task<List<Film>> GetFilmsAvecProjectionsFutures();
        Task<Film> AddFilm(Film film);
        Task<Film> UpdateFilm(Film film);
        Task<List<Projection>> GetProjectionsForFilm(Film film);
        Task<List<Projection>> GetProjectionsFuturesForFilm(Film film);
        Task<bool> DeleteFilm(Film film);
        Task<List<Realisateur>> GetAllRealisateurs();
        Task<List<Acteur>> GetAllActeurs();
        Task<List<Categorie>> GetAllCategories();
    }
}
