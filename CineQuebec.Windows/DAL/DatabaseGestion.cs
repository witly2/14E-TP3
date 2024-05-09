using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL
{
    public class DatabaseGestion
    {
        private IMongoClient mongoDBClient;
        private IMongoDatabase database;

        public DatabaseGestion(IMongoClient client = null)
        {
            Task.Run(async () =>
            {
                mongoDBClient = client ?? await OpenConnection();
                database = await ConnectDatabase();
                await SeedDevelopmentData();
            }).Wait();
        }
        private async Task<IMongoClient> OpenConnection()
        {
            IMongoClient dbClient = null;
            try
            {
                dbClient = new MongoClient("mongodb://localhost:27017/");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de se connecter à la base de données " + ex.Message, "Erreur");
            }
            return dbClient;
        }

        private async Task<IMongoDatabase> ConnectDatabase()
        {
            IMongoDatabase db = null;
            try
            {
                db = mongoDBClient.GetDatabase("TP2DB");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de se connecter à la base de données " + ex.Message, "Erreur");
            }
            return db;
        }
        public List<Abonne> ReadAbonnes()
        {
            var abonnes = new List<Abonne>();

            try
            {
                var collection = database.GetCollection<Abonne>("Abonnes");
                abonnes = collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return abonnes;
        }
        
        public async Task<IMongoCollection<Film>> GetFilmsCollection()
        {
            try
            {
                var filmsCollection = database.GetCollection<Film>("Films");
                return filmsCollection;

            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection " + ex.Message);
            }
        }

        public async Task<IMongoCollection<Person>> GetPersonsCollection()
        {
            try
            {
                var personsCollection = database.GetCollection<Person>("Persons");
                return personsCollection;

            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection " + ex.Message);
            }
        }

        public async Task<IMongoCollection<Employe>> GetEmployesCollection()
        {
            try
            {
                var employesCollection = database.GetCollection<Employe>("Employes");
                return employesCollection;

            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection " + ex.Message);
            }
        }

        public async Task<IMongoCollection<Admin>> GetAdminsCollection()
        {
            try
            {
                var adminsCollection = database.GetCollection<Admin>("Admins");
                return adminsCollection;

            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection " + ex.Message);
            }
        }

        public IMongoCollection<Abonne> GetAbonneCollection()
        {
            return database.GetCollection<Abonne>("Abonnes");
        }

        public async Task<IMongoCollection<Projection>> GetProjectionsCollection()
        {
            try
            {
                var projectionsCollection = database.GetCollection<Projection>("Projections");
                return projectionsCollection;

            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection " + ex.Message);
            }
                    
        }

        public async Task<IMongoCollection<Salle>> GetSallesCollection()
        {
            try
            {
                var sallesCollection = database.GetCollection<Salle>("Salles");
                return sallesCollection;

            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection " + ex.Message);
            }
        }

        public async Task<IMongoCollection<Preference>> GetPreferencesCollection()
        {
            try
            {
                var preferencesCollection = database.GetCollection<Preference>("Preferences");
                return preferencesCollection;

            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Impossible d'obtenir la collection " + ex.Message);
            }
        }

        public async Task SeedDevelopmentData()
        {
            Seed seedData = new Seed(database);
            await seedData.SeedAbonnes();
            await seedData.SeedFilms();
            await seedData.SeedActeurs();
            await seedData.SeedRealisateurs();
            await seedData.SeedCategories();
            await seedData.SeedProjections();
            await seedData.SeedSalles();
            await seedData.SeedAdmins();
        }
    }
}
