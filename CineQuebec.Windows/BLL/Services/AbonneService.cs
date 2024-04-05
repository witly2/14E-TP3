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

        public void AddAbonne(Abonne abonne)
        {
            _abonneRepository.AddAbonne(abonne);
        }

        public void UpdateAbonne(Abonne abonne)
        {
            _abonneRepository.UpdateAbonne(abonne);
        }

        public Abonne GetAbonneByEmail(string email)
        {
            return _abonneRepository.GetByEmail(email);
        }

    }
}
