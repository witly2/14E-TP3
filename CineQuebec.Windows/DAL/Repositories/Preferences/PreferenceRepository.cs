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
            _realisateursCollection = _databaseGestion.GetRealisateursCollection().Result;
            _acteursCollection = _databaseGestion.GetActeursCollection().Result;
            _categoriesCollection = _databaseGestion.GetCategoriesCollection().Result;
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
            var filter = Builders<Preference>.Filter.Eq(p => p.Abonne, abonne);
            return _preferencesCollection.Find(filter).SingleOrDefault();

        }

        public async Task<List<Realisateur>> GetAllRealisateurs(Preference preference)
        {
            var realisateursPreferences = preference?.ListPreferenceRealisateur ?? new List<Realisateur>();
            var allRealisateurs = await _realisateursCollection.Find(_ => true).ToListAsync();
            var realisateursNonPreferes = allRealisateurs.Where(r => !realisateursPreferences.Any(rp => rp.Id == r.Id)).ToList();
            return realisateursNonPreferes;
        }

        public async Task<List<Acteur>> GetAllActeurs(Preference preference)
        {
            var acteursPreferences = preference?.ListPreferenceActeur ?? new List<Acteur>();
            var allActeurs = await _acteursCollection.Find(_ => true).ToListAsync();
            var acteursNonPreferes = allActeurs.Where(r => !acteursPreferences.Any(rp => rp.Id == r.Id)).ToList();
            return acteursNonPreferes;
        }

        public async Task<List<Categorie>> GetAllCategories(Preference preference)
        {
            var categoriesPreferences = preference?.ListPreferenceCategorie ?? new List<Categorie>();
            var allACategories = await _categoriesCollection.Find(_ => true).ToListAsync();
            var categoriesNonPreferes = allACategories.Where(r => !categoriesPreferences.Any(rp => rp.Id == r.Id)).ToList();
            return categoriesNonPreferes;
        }
    }
}
