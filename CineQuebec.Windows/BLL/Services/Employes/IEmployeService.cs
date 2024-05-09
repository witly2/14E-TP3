using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services.Employes
{
    public interface IEmployeService
    {
        Task AddEmploye(Employe employe);
    }
}
