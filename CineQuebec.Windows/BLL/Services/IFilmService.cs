using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services
{
    public interface IFilmService
    {
        Task<List<Film>> GetFilms();
        Task<Film> AddFilm(Film film);
        Task<Film> UpdateFilm(Film film);
        Task<List<Projection>> GetProjections(Film film);
    }
}
