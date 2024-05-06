using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.Repositories.Projections;

public interface IProjectionRepository
{
    Task<List<Projection>> GetProjectionsForFilm(Film film);
    Task<Projection> AddProjection(Projection projection);
    Task<bool> UpdateProjection(Projection projection);
    Task<bool> DeleteProjection(Projection projection);
    Task<List<Salle>> GetSalles();
    Task<List<Projection>> GetProjectionsForSalle(Salle salle, DateTime jour);
}