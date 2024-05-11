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
        Task<Recompense> AjouterRecompenseAvantPremiere(Recompense recompenseExpected);
    }
}
