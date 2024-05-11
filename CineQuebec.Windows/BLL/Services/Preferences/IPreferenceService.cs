using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services.Preferences
{
    public interface IPreferenceService
    {
        void AddPreferenceActeur(Preference preference, Acteur acteur);
        void AddPreferenceCategorie(Preference preference, Categorie categorie);
        void AddPreferenceRealisateur(Preference preference, Realisateur realisateur);
        Preference? GetPreferenceAbonne(Abonne abonne);
        void IsAlreadyInList<T>(Preference preference, T elementAVerifier, Expression<Func<Preference, IEnumerable<T>>> getListExpression);
        void RemovePreference<T>(Preference preference, T elementARetirer, Expression<Func<Preference, IEnumerable<T>>> getListExpression);
        List<Abonne>? GetAbonnesWithThisPreference<T>(T elementAChercher, Expression<Func<Preference, IEnumerable<T>>> getListExpression);
        Task<List<Realisateur>> GetAllRealisateurs(Preference preference);
        Task<List<Acteur>> GetAllActeurs();
        Task<List<Categorie>> GetAllCategories();
    }
}
