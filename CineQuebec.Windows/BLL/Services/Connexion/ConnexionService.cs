using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services.Connexion
{
    public class ConnexionService: IConnexionService
    {
        private readonly IPersonRepository _personRepository;

        public ConnexionService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> GetPersonByEmail(string email)
        {
            return await _personRepository.GetByEmail(email);
        }
    }
}
