using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.BLL.Services
{
    public interface IFilmService
    {
        Task<List<Film>> GetFilms();
        Task<List<Film>> GetFilmsAvecProjectionsFutures();
        Task<Film> AddFilm(Film film);
        Task<Film> UpdateFilm(Film film);
        Task<List<Projection>> GetProjections(Film film);
        Task<bool> DeleteFilm(Film film);
        Task<List<Projection>> GetProjectionsFutures(Film film);
        Task<List<Realisateur>> GetAllRealisateurs();
        Task<List<Acteur>> GetAllActeurs();
        Task<List<Categorie>> GetAllCategories();
    }
}
