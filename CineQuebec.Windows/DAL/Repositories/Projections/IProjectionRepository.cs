using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories.Projections
{
    public interface IProjectionRepository
    {
        Task<List<Projection>> GetProjectionsForFilm(Film film);
        Task<Projection> AddProjection(Projection projection);
        Task<bool> UpdateProjection(Projection projection);
        Task<bool> DeleteProjection(Projection projection);
        Task<List<Salle>> GetSalles();
        Task<List<Salle>> GetSallesDisponibleForProjection(Film film, DateTime debutProjectionSouhaite);
    }
}
