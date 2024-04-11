using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnés;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services
{
    public class AbonneService
    {

        private readonly IAbonneRepsitory _abonneRepository;

        public AbonneService(IAbonneRepsitory abonneRepsitory)
        {
                _abonneRepository = abonneRepsitory;
        }

        public List<Abonne> GetAbonnes()
        {
            return _abonneRepository.GetAbonnes();
        }

        public async Task AddAbonne(Abonne abonne)
        {
           await _abonneRepository.AddAbonne(abonne);
        }

        public async Task UpdateAbonne(Abonne abonne)
        {
           await _abonneRepository.UpdateAbonne(abonne);
        }

        public async Task<Abonne> GetAbonneByEmail(string email)
        {
            return await _abonneRepository.GetByEmail(email);
        }

    }
}
