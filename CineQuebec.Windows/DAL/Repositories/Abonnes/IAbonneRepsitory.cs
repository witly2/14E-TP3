using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
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
        Task AddAbonne(Abonne abonne);

        Task<Abonne> GetByEmail(string email);
    }
}
