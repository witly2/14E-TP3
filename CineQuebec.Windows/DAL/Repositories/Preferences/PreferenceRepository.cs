using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace CineQuebec.Windows.DAL.Repositories.Preferences
{
    public class PreferenceRepository : IPreferenceRepository
    {
        private readonly DatabaseGestion _databaseGestion;
        private readonly IMongoCollection<Preference> _preferencesCollection;
        private readonly IMongoCollection<Realisateur> _realisateursCollection;
        private readonly IMongoCollection<Acteur> _acteursCollection;
        private readonly IMongoCollection<Categorie> _categoriesCollection;

        public PreferenceRepository(DatabaseGestion databaseGestion)
        {
            _databaseGestion = databaseGestion;
            _preferencesCollection = _databaseGestion.GetPreferencesCollection().Result;
        }

        public bool IsAlreadyInList<T>(Preference preference, T elementAVerifier, Expression<Func<Preference, IEnumerable<T>>> getListExpression)
        {
            try
            {
                var filterBuilder = Builders<Preference>.Filter
                    .Where(p => p.Id == preference.Id && getListExpression.Compile()(p).Any(t => t.Equals(elementAVerifier)));
               
                return _preferencesCollection.Find(filterBuilder).Any();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Erreur lors de la recherche si le réalisateur est dans la liste de préférence : " + ex.Message);
            }
        }

        public void UpdatePreference(Preference preference)
        {
            try
            {
                var filter = Builders<Preference>.Filter.Eq(p => p.Id, preference.Id);
                var updatePreference = Builders<Preference>.Update
                    .Set(p => p.ListPreferenceRealisateur, preference.ListPreferenceRealisateur)
                    .Set(p => p.ListPreferenceActeur, preference.ListPreferenceActeur)
                    .Set(p => p.ListPreferenceCategorie, preference.ListPreferenceCategorie);

                _preferencesCollection.UpdateOne(filter, updatePreference);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Erreur lors de la mise à jour des préférences : " + ex.Message);
            }   
        }

        public List<Abonne> GetAbonnesWithThisPreference<T>(T elementAChercher, Expression<Func<Preference, IEnumerable<T>>> getListExpression)
        {
            try
            {
               var abonnesAvecPreference = new List<Abonne>();

               foreach (var preference in _preferencesCollection.AsQueryable())
               {
                    var getList = getListExpression.Compile()(preference);
                    if (getList != null && getList.Any(t => t.Equals(elementAChercher)))
                    {
                        abonnesAvecPreference.AddRange((IEnumerable<Abonne>)preference.Abonne);
                    }
               }

               return abonnesAvecPreference;
            }
            catch (Exception ex)
            {
               throw new InvalidDataException("Erreur lors de la recherche des abonnés avec cette préférence : " + ex.Message);
            }
        }

        public Preference GetPreferenceAbonne(Abonne abonne)
        {
            try
            {
                var filter = Builders<Preference>.Filter.Eq(p => p.Abonne, abonne);
                return _preferencesCollection.Find(filter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la récupération des préférences de l'abonné : ", ex);
            }
        }

        public async Task<List<Realisateur>> GetAllRealisateurs()
        {
            try
            {
                return await _realisateursCollection.Aggregate().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection de réalisateurs : " + ex.Message);
            }
        }

        public async Task<List<Acteur>> GetAllActeurs()
        {
            try
            {
                return await _acteursCollection.Aggregate().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection d'acteurs : " + ex.Message);
            }
        }

        public async Task<List<Categorie>> GetAllCategories()
        {
            try
            {
                return await _categoriesCollection.Aggregate().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection des catégories : " + ex.Message);
            }
        }
    }
}
