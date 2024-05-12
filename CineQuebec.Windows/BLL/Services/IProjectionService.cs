using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.BLL.Services
{
    public interface IProjectionService
    {
        Task<List<Projection>> GetProjections(Film film);
        Task<Projection> AddProjection(Projection projection);
        Task<bool> UpdateProjection(Projection projection);
        Task<bool> DeleteProjection(Projection projection);
        Task<List<Salle>> GetSalles();
        Task<List<Salle>> GetSallesDisponibles(Film film, DateTime debutProjectionSouhaite);
    }
}
