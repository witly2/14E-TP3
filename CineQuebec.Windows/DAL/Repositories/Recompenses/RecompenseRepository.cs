using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories.Recompenses
{
    public class RecompenseRepository : IRecompenseRepository
    {
        public Task<Recompense> AjouterRecompenseAvantPremiere(Recompense recompense)
        {
            throw new NotImplementedException();
        }

        public Task<Recompense> AjouterRecompenseTicketGratuit(Recompense recompense)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountPlaceRestante(Recompense recompense)
        {
            throw new NotImplementedException();
        }

        public Task<List<Recompense>> GetRecompenseAbonne(Abonne abonne)
        {
            throw new NotImplementedException();
        }
    }
}
