using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.Repositories.Films;

public interface IFilmRepository
{
    Task<List<Film>> GetFilms();
    Task<Film> AddFilm(Film film);
    Task<Film> UpdateFilm(Film film);
    Task<List<Projection>> GetProjectionsForFilm(Film film);
    Task<bool> DeleteFilm(Film film);
}