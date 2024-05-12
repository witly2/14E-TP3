using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CineQuebec.Windows.DAL.Repositories.Recompenses
{
    public class RecompenseRepository : IRecompenseRepository
    {
        private readonly DatabaseGestion _databaseGestion;
        private readonly IMongoCollection<Recompense> _recompensesCollection;

        public RecompenseRepository(DatabaseGestion databaseGestion)
        {
            _databaseGestion = databaseGestion;
            _recompensesCollection = _databaseGestion.GetRecompensesCollection().Result;
        }
        public async Task<List<Recompense>> GetAllRecompenses()
        {
            try
            {
                return await _recompensesCollection.Aggregate().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection de recompenses : " + ex.Message);
            }
        }
        public async Task<Recompense> AjouterRecompense(Recompense recompense)
        {
            try
            {
                await _recompensesCollection.InsertOneAsync(recompense);
                return recompense;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'ajouter la recompense : " + ex.Message);
            }
        }

        public async Task<List<Recompense>> GetRecompenseAbonne(Abonne abonne)
        {
            try
            {
                List<Abonne> abonneCherche = new List<Abonne> { abonne };
                var recompenses = Builders<Recompense>.Filter.AnyIn(r => r.Abonne, abonneCherche);
                return await _recompensesCollection.Find(recompenses).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection de recompenses : " + ex.Message);
            }
        }
    }
}
