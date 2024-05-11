using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Repositories.Abonnés;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories.Abonnes
{
    public class AbonneRepsitory : IAbonneRepsitory
    {

        private readonly DatabaseGestion _databaseGestion;
        private readonly IMongoCollection<Abonne> _abonneCollection;
        private readonly IMongoCollection<Person> _personsCollection;
        private readonly IMongoCollection<Preference> _preferencesCollection;


        public AbonneRepsitory(DatabaseGestion databaseGestion)
        {
            _databaseGestion = databaseGestion;
            _abonneCollection = _databaseGestion.GetAbonneCollection();
            _personsCollection = _databaseGestion.GetPersonsCollection().Result;
            _preferencesCollection = _databaseGestion.GetPreferencesCollection().Result;
        }

        public async Task AddAbonne(Abonne abonne)
        {
            if (_abonneCollection.Find(x => x.Email == abonne.Email).Any())
            {
               throw new EmailExisteExeption("Un abonnée existe dejà avec ce courriel");
            }
            Person person = new Person
            {
                Nom = abonne.Nom,
                Prenom = abonne.Prenom,
                Email = abonne.Email,
                Password = abonne.Password,
                Salt = abonne.Salt
            };

            abonne.SetId(person.Id);
            await _personsCollection.InsertOneAsync(person);
            await _abonneCollection.InsertOneAsync(abonne);

            Preference preference = new Preference();
            preference.SetAbonne(abonne);
            List<Realisateur> realisateurs = new List<Realisateur>();
            preference.SetListPreferenceRealisateur(realisateurs);
            List<Acteur> acteurs = new List<Acteur>();
            preference.SetListPreferenceActeur(acteurs);
            List<Categorie> categories = new List<Categorie>();
            preference.SetListPreferenceCategorie(categories);
            await _preferencesCollection.InsertOneAsync(preference);
        }

        public List<Abonne> GetAbonnes()
        {
            var abonnes = new List<Abonne>();

            try
            {
                
                abonnes = _abonneCollection.Aggregate().ToList().OrderBy(x=>x.Username).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return abonnes;
        }

        public async Task<Abonne> GetByEmail(string email)
        {
            if (!_abonneCollection.Find(x => x.Email == email).Any())
            {
                throw new EmailNotExiseException("Auccun abonné ne possède ce courriel");
            }
            return await _abonneCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
