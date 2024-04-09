using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Projections;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services
{
    public class ProjectionService : IProjectionService
    {
        private readonly IProjectionRepository _projectionRepository;
        public ProjectionService(IProjectionRepository projectionRepository)
        {
            _projectionRepository = projectionRepository;
        }

        public async Task<Projection> AddProjection(Projection projection)
        {
            return await _projectionRepository.AddProjection(projection);
        }

        public async Task<bool> DeleteProjection(Projection projection)
        {
            return await _projectionRepository.DeleteProjection(projection);
        }

        public async Task<List<Projection>> GetProjections(ObjectId filmId)
        {
            return await _projectionRepository.GetProjectionsForFilm(filmId);
        }

        public async Task<bool> UpdateProjection(Projection projection)
        {
            return await _projectionRepository.UpdateProjection(projection);
        }
    }
}
