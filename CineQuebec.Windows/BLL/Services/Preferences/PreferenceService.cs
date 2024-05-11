using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Repositories.Preferences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services.Preferences
{
    public class PreferenceService: IPreferenceService
    {
        private readonly IPreferenceRepository _preferenceRepository;

        public PreferenceService(IPreferenceRepository preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }

        public void AddPreferenceActeur(Preference preference, Acteur acteur)
        {
            if (preference.ListPreferenceActeur.Count >= 5)
            {
                throw new MaximumPreferenceException("La limite de 5 acteurs préférés est atteinte.");
            }

            preference.ListPreferenceActeur.Add(acteur);
            _preferenceRepository.UpdatePreference(preference);
        }

        public void AddPreferenceCategorie(Preference preference, Categorie categorie)
        {
            if (preference.ListPreferenceCategorie.Count >= 3)
            {
                throw new MaximumPreferenceException("La limite de 3 catégories préférés est atteinte.");
            }

            preference.ListPreferenceCategorie.Add(categorie);
            _preferenceRepository.UpdatePreference(preference);
        }

        public void AddPreferenceRealisateur(Preference preference, Realisateur realisateur)
        {
            if(preference != null)
            {
                if (preference.ListPreferenceRealisateur.Count >= 5)
                {
                    throw new MaximumPreferenceException("La limite de 5 réalisateurs préférés est atteinte.");
                }
                preference.ListPreferenceRealisateur.Add(realisateur);
                _preferenceRepository.UpdatePreference(preference);
            } 
            else
            { 
                preference.ListPreferenceRealisateur.Add(realisateur);
                _preferenceRepository.UpdatePreference(preference);
            }
        }

        public List<Abonne>? GetAbonnesWithThisPreference<T>(T elementAChercher, Expression<Func<Preference, IEnumerable<T>>> getListExpression)
        {
            try
            {
                return _preferenceRepository.GetAbonnesWithThisPreference(elementAChercher, getListExpression);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("UneUne erreur est survenue lors de la récupération des abonnés avec cette préférence : ", ex);
            }
        }

        public Preference GetPreferenceAbonne(Abonne abonne)
        {
            var preferences = _preferenceRepository.GetPreferenceAbonne(abonne);
            return preferences;
        }

        public void IsAlreadyInList<T>(Preference preference, T elementAVerifier, Expression<Func<Preference, IEnumerable<T>>> getListExpression)
        {
            if (_preferenceRepository.IsAlreadyInList(preference, elementAVerifier, getListExpression))
            {
                throw new PreferenceAlreadyExistsException("Ce choix est déjà dans la liste de préférence.");
            }
        }

        public void RemovePreference<T>(Preference preference, T elementARetirer, Expression<Func<Preference, IEnumerable<T>>> getListExpression)
        {
            if (preference == null)
            {
                throw new ArgumentNullException(nameof(preference));
            }

            if (elementARetirer == null)
            {
                throw new ArgumentNullException(nameof(elementARetirer));
            }

            try
            {
                var getListFunc = getListExpression.Compile();
                var list = getListFunc(preference).ToList();
                list.Remove(elementARetirer);

                if (typeof(T) == typeof(Realisateur))
                {
                    preference.SetListPreferenceRealisateur(list.Cast<Realisateur>().ToList());
                }
                else if (typeof(T) == typeof(Acteur))
                {
                    preference.SetListPreferenceActeur(list.Cast<Acteur>().ToList());
                }
                else if (typeof(T) == typeof(Categorie))
                {
                    preference.SetListPreferenceCategorie(list.Cast<Categorie>().ToList());
                }

                _preferenceRepository.UpdatePreference(preference);
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la suppression du réalisateur de la liste de préférence : ", ex);
            }
        }

        public async Task<List<Acteur>> GetAllActeurs()
        {

            try
            {
                return await _preferenceRepository.GetAllActeurs();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Une erreur s'est produite lors de la récupération des acteurs : " + ex.Message);
            }
        }

        public async Task<List<Categorie>> GetAllCategories()
        {
            try
            {
                return await _preferenceRepository.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Une erreur s'est produite lors de la récupération des catégories : " + ex.Message);
            }
        }

        public async Task<List<Realisateur>> GetAllRealisateurs(Preference preference)
        {
            try
            {
                return await _preferenceRepository.GetAllRealisateurs(preference);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Une erreur s'est produite lors de la récupération des réalisateurs : " + ex.Message);
            }
        }
    }
}
