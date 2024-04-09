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
            return await _filmRepository.GetFilms();
        }

        public async Task<Film> AddFilm(Film film)
        {
           return await _filmRepository.AddFilm(film);
        }

        public async Task<Film> UpdateFilm(Film film)
        {
            return await _filmRepository.UpdateFilm(film);
        }

        public async Task<List<Projection>> GetProjections(ObjectId filmId)
        {
            return await _filmRepository.GetProjectionsForFilm(filmId);
        }
    }
}
