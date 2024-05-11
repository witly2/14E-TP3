using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Recompenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services.Recompenses
{
    public class RecompenseService : IRecompenseService
    {
        private readonly IRecompenseRepository _recompenseRepository;

        public Task<Recompense> AjouterRecompenseAvantPremiere(Recompense recompenseExpected)
        {
            throw new NotImplementedException();
        }

        public Task<Recompense> AjouterRecompenseTicketGratuit(Recompense recompenseExpected)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountPlaceRestante(Recompense recompense)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountRecompenseAbonne(Abonne abonne)
        {
            throw new NotImplementedException();
        }
    }
}
