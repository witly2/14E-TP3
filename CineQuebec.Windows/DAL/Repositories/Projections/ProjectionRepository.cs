using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories.Projections
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly DatabaseGestion _databaseGestion;
        private readonly IMongoCollection<Projection> _projectionsCollection;
        private readonly IMongoCollection<Salle> _sallesCollection;

        public ProjectionRepository(DatabaseGestion databaseGestion)
        {
            _databaseGestion = databaseGestion;
            _projectionsCollection = _databaseGestion.GetProjectionsCollection().Result;
            _sallesCollection = _databaseGestion.GetSallesCollection().Result;
        }
        public async Task<Projection> AddProjection(Projection projection)
        {
            try
            {
                await _projectionsCollection.InsertOneAsync(projection);
                return projection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'ajouter la projection : " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteProjection(Projection projection)
        {
            try
            {
                var deleteResult = await _projectionsCollection.DeleteOneAsync(p => p.Id == projection.Id);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de supprimer la projection : " + ex.Message);
                return false;
            }
        }

        public async Task<List<Projection>> GetProjectionsForFilm(Film film)
        {
            try
            {
                var projectionsFilm = Builders<Projection>.Filter.Eq(p => p.Film.Id, film.Id);
                return await _projectionsCollection.Find(projectionsFilm).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir les projections pour le film : " + ex.Message);
                return new List<Projection>();
            }
        }

        public async Task<bool> UpdateProjection(Projection projection)
        {
            try
            {
                var filter = Builders<Projection>.Filter.Eq(p => p.Id, projection.Id);
                var updateResult = await _projectionsCollection.ReplaceOneAsync(filter, projection);

                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de mettre à jour la projection : " + ex.Message);
                return false;
            }
        }

        public async Task<List<Salle>> GetSalles()
        {
            try
            {
                var resultSalles = await _sallesCollection.Aggregate().ToListAsync();
                return resultSalles;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir les salles : " + ex.Message);
                return new List<Salle>();
            }
        }

        public async Task<List<Salle>> GetSallesDisponibleForProjection(Film film, DateTime debutProjectionSouhaite)
        {
            try
            {
                DateTime finProjectionAddCleanSalleAfter = debutProjectionSouhaite.AddMinutes(film.Duration).AddHours(1);
                DateTime addTimeCleanSalleDebutProjection = debutProjectionSouhaite.AddHours(-1);

                var filterProjectionsEnCours = Builders<Projection>.Filter.And(
                    Builders<Projection>.Filter.Lt(p => p.DateHeureDebut, finProjectionAddCleanSalleAfter),
                    Builders<Projection>.Filter.Gt(p => p.DateHeureDebut, addTimeCleanSalleDebutProjection),
                    Builders<Projection>.Filter.Ne(p => p.DateHeureDebut, debutProjectionSouhaite));

                var filterProjectionsFinissantAvant = Builders<Projection>.Filter.And(
                    Builders<Projection>.Filter.Lt(p => p.DateHeureFin, finProjectionAddCleanSalleAfter),
                    Builders<Projection>.Filter.Gt(p => p.DateHeureFin, addTimeCleanSalleDebutProjection),
                    Builders<Projection>.Filter.Ne(p => p.DateHeureFin, debutProjectionSouhaite));

                var filter = Builders<Projection>.Filter.Or(
                    filterProjectionsEnCours, filterProjectionsFinissantAvant);

                var projectionsEnCoursOuFinissantAvant = await _projectionsCollection.Find(filter).ToListAsync();
                var sallesOccupees = projectionsEnCoursOuFinissantAvant.Select(p => p.Salle).ToList();

                var toutesSallesExistantes = await GetSalles();

                var sallesDisponibles = toutesSallesExistantes.Except(sallesOccupees).ToList();

                return sallesDisponibles;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir les salles disponible à cette heure : " + ex.Message);
                return new List<Salle>();
            }
        }
    }
}
