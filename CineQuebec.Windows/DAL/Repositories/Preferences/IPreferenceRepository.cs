using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories.Preferences
{
    public interface IPreferenceRepository
    {
        bool IsRealisateurAlreadyInList(Preference preference, Realisateur realisateur);
        void UpdatePreference(Preference preference);
    }
}
