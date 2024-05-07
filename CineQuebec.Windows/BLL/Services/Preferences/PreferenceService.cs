using CineQuebec.Windows.DAL.Data;
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
            throw new NotImplementedException();
        }
    }
}
