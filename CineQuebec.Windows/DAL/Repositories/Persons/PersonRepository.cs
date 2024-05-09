using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Repositories.Abonnés;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
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
        private IMongoCollection<Person> _personCollection;
        private IMongoCollection<Abonne> _abonnesCollection;
        private IMongoCollection<Admin> _adminsCollection;
        private IMongoCollection<Employe> _employesCollection;

        public PersonRepository(DatabaseGestion database)
        {
            _databaseGestion = database;
            _abonnesCollection = _databaseGestion.GetAbonneCollection();
            InitializeCollectionAsync();
        }
        private async void InitializeCollectionAsync()
        {
            _personCollection = await _databaseGestion.GetPersonsCollection();
            _adminsCollection = await _databaseGestion.GetAdminsCollection();
            _employesCollection = await _databaseGestion.GetEmployesCollection();
        }

        public async Task<Person> GetByEmail(string email)
        {
            var personne =  await _personCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
            if (personne != null)
            {
                var abonne = await _abonnesCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
                if (abonne != null)
                    return abonne;

                var admin = await _adminsCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
                if (admin != null)
                    return admin;

                var employe = await _employesCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
                if (employe != null)
                    return employe;

            } 
            else
            {
                throw new EmailNotExiseException("Aucune personne ne possède ce courriel");
            }
            return null;
        }
    }
}
