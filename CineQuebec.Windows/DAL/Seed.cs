using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL
{
    public class Seed
    {

        private readonly IMongoCollection<Abonne> _abonnesCollection;

        public Seed(IMongoDatabase database)
        {
            _abonnesCollection = database.GetCollection<Abonne>("Abonnes");
        }


        /// <summary>
        ///  Liste de données pour les abonnés
        /// </summary>
        public void SeedAbonnes()
        {

            if (!_abonnesCollection.Indexes.List().Any())
            {
                var abonnes = new List<Abonne>
                {
                    new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = DateTime.UtcNow },
                    new Abonne { Username = "Jane Smith", Email = "jane.smith@example.com", DateAdhesion = DateTime.UtcNow },
                    new Abonne { Username = "Alice Johnson", Email = "alice.johnson@example.com", DateAdhesion = DateTime.UtcNow },
                    new Abonne { Username = "Bob Brown", Email = "bob.brown@example.com", DateAdhesion = DateTime.UtcNow },
                    new Abonne { Username = "Michael Wilson", Email = "michael.wilson@example.com", DateAdhesion = DateTime.UtcNow },
                    new Abonne { Username = "Emily Davis", Email = "emily.davis@example.com", DateAdhesion = DateTime.UtcNow },
                    new Abonne { Username = "Daniel Martinez", Email = "daniel.martinez@example.com", DateAdhesion = DateTime.UtcNow },
                    new Abonne { Username = "Sarah Anderson", Email = "sarah.anderson@example.com", DateAdhesion = DateTime.UtcNow },
                    new Abonne { Username = "David Taylor", Email = "david.taylor@example.com", DateAdhesion = DateTime.UtcNow },
                    new Abonne { Username = "Olivia Thomas", Email = "olivia.thomas@example.com", DateAdhesion = DateTime.UtcNow }
                };

                _abonnesCollection.InsertMany(abonnes);
                Console.WriteLine("Données d'abonnés insérées avec succès.");
            }



        }
    }
}
