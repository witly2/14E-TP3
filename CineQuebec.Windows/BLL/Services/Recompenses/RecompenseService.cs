using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Recompenses;

namespace CineQuebec.Windows.BLL.Services.Recompenses
{
    public class RecompenseService : IRecompenseService
    {
        private readonly IRecompenseRepository _recompenseRepository;

        public RecompenseService(IRecompenseRepository recompenseRepository)
        {
            _recompenseRepository = recompenseRepository;
        }

        public async Task<List<Recompense>> GetAllRecompenses()
        {
            var recompenses = await _recompenseRepository.GetAllRecompenses();
            return recompenses;
        }

        public async Task<Recompense> AjouterRecompense(Recompense recompense)
        {
            if(recompense == null)
            {
                throw new ArgumentNullException(nameof(recompense), "La récompense ne peut pas être null.");
            }
            var recomprenseBd = await _recompenseRepository.AjouterRecompense(recompense);
            return recomprenseBd;
        }

        public async Task<int> GetCountPlaceRestante(Recompense recompense)
        {
            if (recompense == null)
            {
                throw new ArgumentNullException(nameof(recompense), "La récompense ne peut pas être null.");
            }
            int nombreAbonneRecompense = recompense.Abonne.Count();
            int nombrePlaceRestante = recompense.NombrePlace - nombreAbonneRecompense;
            return nombrePlaceRestante;
        }

        public async Task<int> GetCountRecompenseAbonne(Abonne abonne)
        {
            if (abonne == null)
            {
                throw new ArgumentNullException(nameof(abonne), "L'abonné ne peut pas être null.");
            } 
            else
            {
                List<Recompense> recompenses = new List<Recompense>();
                recompenses = await _recompenseRepository.GetRecompenseAbonne(abonne);
                int nombreRecompenseAbonne = recompenses.Count();
                return nombreRecompenseAbonne;
            }
        }
    }
}
