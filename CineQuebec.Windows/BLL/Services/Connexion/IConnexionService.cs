using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services.Connexion
{
    public interface IConnexionService
    {
        Task<Person> GetPersonByEmail(string email);

    }
}
