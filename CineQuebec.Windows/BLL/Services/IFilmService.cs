using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.BLL.Services
{
    public interface IFilmService
    {
        Task<List<Film>> GetFilms();
        Task<Film> AddFilm(Film film);
        Task<Film> UpdateFilm(Film film);
        Task<List<Projection>> GetProjections(Film film);
        Task<bool> DeleteFilm(Film film);
    }
}
