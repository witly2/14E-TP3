using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services.Recompenses
{
    public interface IRecompenseService
    {
        Task<Recompense> AjouterRecompense(Recompense recompense);
        Task<List<Recompense>> GetAllRecompenses();
        Task<int> GetCountPlaceRestante(Recompense recompense);
        Task<int> GetCountRecompenseAbonne(Abonne abonne);
    }
}
