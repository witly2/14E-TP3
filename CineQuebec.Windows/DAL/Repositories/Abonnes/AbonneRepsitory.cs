using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnés;
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


        public AbonneRepsitory(DatabaseGestion databaseGestion)
        {
            _databaseGestion = databaseGestion;
            _abonneCollection = _databaseGestion.GetAbonneCollection();
        }

        public void AddAbonne(Abonne abonne)
        {
            try
            {
                _abonneCollection.InsertOne(abonne);
                Console.WriteLine("Congrats AddAbonne");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'ajouter l'abonné : " + ex.Message, "Erreur");
            }
        }

        public List<Abonne> GetAbonnes()
        {
            var abonnes = new List<Abonne>();

            try
            {
                
                abonnes = _abonneCollection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return abonnes;
        }

        public void UpdateAbonne(Abonne abonne)
        {
            throw new NotImplementedException();
        }
    }
}
