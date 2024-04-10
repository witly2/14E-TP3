using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Films;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Services
{
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
                Console.WriteLine($"Une erreur s'est produite lors de la récupération des films : " + ex.Message);
                throw;
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
                Console.WriteLine($"Une erreur s'est produite lors de l'ajout du film : " + ex.Message);
                throw;
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
                Console.WriteLine($"Une erreur s'est produite lors de la récupération du film : " + ex.Message);
                throw;
            }
        }

        public async Task<List<Projection>> GetProjections(ObjectId filmId)
        {
            try
            {
                return await _filmRepository.GetProjectionsForFilm(filmId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de la récupération des projections du film : " + ex.Message);
                throw;
            }
        }
    }
}
