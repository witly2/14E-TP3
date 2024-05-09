using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories.Persons
{
    public class PersonRepository: IPersonRepository
    {
        private readonly DatabaseGestion _databaseGestion;
        private readonly IMongoCollection<Person> _personCollection;

        public PersonRepository(DatabaseGestion database)
        {
            _databaseGestion = database;
            _personCollection = (IMongoCollection<Person>?)_databaseGestion.GetPersonsCollection();
    }

        public async Task<Person> GetByEmail(string email)
        {
            return await _personCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
