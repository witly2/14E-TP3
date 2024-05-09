using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories.Employes
{
    public class EmployeRepository : IEmployeRepository
    {
        private readonly DatabaseGestion _databaseGestion;
        private readonly IMongoCollection<BsonDocument> _sequenceCollection;
        private readonly IMongoCollection<Employe> _employesCollection;

        public EmployeRepository(DatabaseGestion databaseGestion)
        {
            _databaseGestion = databaseGestion;
            _employesCollection = _databaseGestion.GetEmployesCollection().Result;
        }

        public async Task AddEmploye(Employe employe)
        {
            if (_employesCollection.Find(x => x.Email == employe.Email).Any())
            {
                throw new EmailExisteExeption("Un employe existe dejà avec ce courriel");
            }
            await _employesCollection.InsertOneAsync(employe);
        }
    }
}
