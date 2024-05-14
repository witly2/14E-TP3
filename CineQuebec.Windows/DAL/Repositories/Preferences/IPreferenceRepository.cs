using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories.Preferences
{
    public interface IPreferenceRepository
    {
        List<Abonne> GetAbonnesWithThisPreference<T>(T elementAChercher, Expression<Func<Preference, IEnumerable<T>>> getListExpression);
        Preference GetPreferenceAbonne(Abonne abonne);
        bool IsAlreadyInList<T>(Preference preference, T elementAVerifier, Expression<Func<Preference, IEnumerable<T>>> getListExpression);
        void UpdatePreference(Preference preference);
        Task<List<Realisateur>> GetAllRealisateurs(Preference preference);
        Task<List<Acteur>> GetAllActeurs(Preference preference);
        Task<List<Categorie>> GetAllCategories(Preference preference);
        List<Abonne>? GetAbonnesWithThisPreferenceCategorie(Categorie categorie);
        List<Abonne>? GetAbonnesWithThisPreferenceRealisateur(Realisateur realisateur);
        List<Abonne>? GetAbonnesWithThisPreferenceActeur(Acteur acteur);
    }
}
