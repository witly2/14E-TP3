using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Repositories.Preferences;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddPreferenceRealisateur(Preference preference, Realisateur realisateur)
        {
            if(preference.ListPreferenceRealisateur.Count >= 5)
            {
                throw new MaximumPreferenceException("La limite de 5 réalisateurs préférés est atteinte.");
            }

            if(_preferenceRepository.IsRealisateurAlreadyInList(preference, realisateur))
            {
                throw new PreferenceAlreadyExistsException("Le réalisateur est déjà dans la liste de préférence.");
            }

            preference.ListPreferenceRealisateur.Add(realisateur);
            _preferenceRepository.UpdatePreference(preference);
        }
    }
}
