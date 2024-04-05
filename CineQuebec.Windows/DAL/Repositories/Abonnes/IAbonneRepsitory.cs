using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories.Abonnés
{
    public interface IAbonneRepsitory
    {

        List<Abonne> GetAbonnes();
        void AddAbonne(Abonne abonne);
        void UpdateAbonne(Abonne abonne);

        Abonne GetByEmail(string email);
    }
}
