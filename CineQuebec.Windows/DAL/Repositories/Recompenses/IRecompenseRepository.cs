using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories.Recompenses
{
    public interface IRecompenseRepository
    {
        Task<Recompense> AjouterRecompenseAvantPremiere(Recompense recompense);
    }
}
