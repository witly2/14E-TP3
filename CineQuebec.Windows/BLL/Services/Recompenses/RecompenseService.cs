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

        public RecompenseService(IRecompenseRepository  recompenseRepository)
        {
            _recompenseRepository = recompenseRepository;
        }
    }
}
