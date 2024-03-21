using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL
{
    public class Seed
    {

        private readonly IMongoCollection<Abonne> _abonnesCollection;
        private readonly IMongoCollection<Film> _filmsCollection;

        public Seed(IMongoDatabase database)
        {
            _abonnesCollection = database.GetCollection<Abonne>("Abonnes");
            _filmsCollection = database.GetCollection<Film>("films");
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
                    new Abonne { Username = "John Doe", Email = "john.doe@example.com", DateJoin = DateTime.UtcNow },
                    new Abonne { Username = "Jane Smith", Email = "jane.smith@example.com", DateJoin = DateTime.UtcNow },
                    new Abonne { Username = "Alice Johnson", Email = "alice.johnson@example.com", DateJoin = DateTime.UtcNow },
                    new Abonne { Username = "Bob Brown", Email = "bob.brown@example.com", DateJoin = DateTime.UtcNow },
                    new Abonne { Username = "Michael Wilson", Email = "michael.wilson@example.com", DateJoin = DateTime.UtcNow },
                    new Abonne { Username = "Emily Davis", Email = "emily.davis@example.com", DateJoin = DateTime.UtcNow },
                    new Abonne { Username = "Daniel Martinez", Email = "daniel.martinez@example.com", DateJoin = DateTime.UtcNow },
                    new Abonne { Username = "Sarah Anderson", Email = "sarah.anderson@example.com", DateJoin = DateTime.UtcNow },
                    new Abonne { Username = "David Taylor", Email = "david.taylor@example.com", DateJoin = DateTime.UtcNow },
                    new Abonne { Username = "Olivia Thomas", Email = "olivia.thomas@example.com", DateJoin = DateTime.UtcNow }
                };

                _abonnesCollection.InsertMany(abonnes);
                Console.WriteLine("Données d'abonnés insérées avec succès.");
            }

        }

        /// <summary>
        /// Liste de données pour les films
        /// </summary>
        public void SeedFilms()
        {
            if (!_filmsCollection.Indexes.List().Any())
            {
                var films = new List<Film>
                {
                    new Film { OriginalTitle = "Inception", FrenchTitle = "Inception", Description = "A thief who enters the dreams of others to steal their secrets from their subconscious.", Duration = 148, InternationalReleaseDate = new DateTime(2010, 7, 16), Rating = 8, OriginalLanguage = "English" },
                    new Film { OriginalTitle = "The Dark Knight", FrenchTitle = "Le Chevalier Noir", Description = "When the menace known as The Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", Duration = 152, InternationalReleaseDate = new DateTime(2008, 7, 18), Rating = 9, OriginalLanguage = "English" },
                    new Film { OriginalTitle = "Interstellar", FrenchTitle = "Interstellaire", Description = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.", Duration = 169, InternationalReleaseDate = new DateTime(2014, 11, 7), Rating = 8, OriginalLanguage = "English" },
                    new Film { OriginalTitle = "La La Land", FrenchTitle = "La La Land", Description = "While navigating their careers in Los Angeles, a pianist and an actress fall in love while attempting to reconcile their aspirations for the future.", Duration = 128, InternationalReleaseDate = new DateTime(2016, 12, 25), Rating = 8, OriginalLanguage = "English" },
                    new Film { OriginalTitle = "Dune", FrenchTitle = "Dune", Description = "Feature adaptation of Frank Herbert's science fiction novel, about the son of a noble family entrusted with the protection of the most valuable asset and most vital element in the galaxy.", Duration = 155, InternationalReleaseDate = new DateTime(2023, 9, 21), Rating = 7, OriginalLanguage = "English" }
                };

                _filmsCollection.InsertMany(films);
                Console.WriteLine("Données de films insérées avec succès.");
            }
        }
    }
}
