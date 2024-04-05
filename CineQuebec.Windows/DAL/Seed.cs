using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL
{
    public class Seed
    {

        private readonly IMongoCollection<Abonne> _abonnesCollection;
        private readonly IMongoCollection<Film> _filmsCollection;
        private readonly IMongoCollection<Acteur> _acteursCollection;
        private readonly IMongoCollection<Realisateur> _realisateursCollection;
        private readonly IMongoCollection<Categorie> _categoriesCollection;
        private readonly IMongoCollection<Salle> _sallesCollection;
        private readonly IMongoCollection<Projection> _projectionsCollection;

        public Seed(IMongoDatabase database)
        {
            _abonnesCollection = database.GetCollection<Abonne>("Abonnes");
            _filmsCollection = database.GetCollection<Film>("Films");
            _acteursCollection = database.GetCollection<Acteur>("Acteurs");
            _realisateursCollection = database.GetCollection<Realisateur>("Realisateurs");
            _categoriesCollection = database.GetCollection<Categorie>("Categories");
            _sallesCollection = database.GetCollection<Salle>("Salles");
            _projectionsCollection = database.GetCollection<Projection>("Projections");
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

        /// <summary>
        /// Liste de données pour les films
        /// </summary>
        public void SeedFilms()
        {
            if (!_filmsCollection.Indexes.List().Any())
            {
                var films = new List<Film>
                {
                    new Film { OriginalTitle = "Inception", FrenchTitle = "Inception", Description = "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", Duration = 148, InternationalReleaseDate = new DateTime(2010, 7, 16), Rating = 8, OriginalLanguage = "English" },
                    new Film { OriginalTitle = "The Dark Knight", FrenchTitle = "Le Chevalier Noir", Description = "Lorsque la menace connue sous le nom du Joker sème le chaos parmi les habitants de Gotham, Batman doit accepter l'une des plus grandes épreuves psychologiques et physiques de sa capacité à lutter contre l'injustice.", Duration = 152, InternationalReleaseDate = new DateTime(2008, 7, 18), Rating = 9, OriginalLanguage = "English" },
                    new Film { OriginalTitle = "Interstellar", FrenchTitle = "Interstellaire", Description = "Une équipe d'explorateurs voyage à travers un trou de ver dans l'espace dans le but de garantir la survie de l'humanité.", Duration = 169, InternationalReleaseDate = new DateTime(2014, 11, 7), Rating = 8, OriginalLanguage = "English" },
                    new Film { OriginalTitle = "La La Land", FrenchTitle = "La La Land", Description = "En naviguant dans leur carrière à Los Angeles, un pianiste et une actrice tombent amoureux tout en tentant de concilier leurs aspirations pour l'avenir.", Duration = 128, InternationalReleaseDate = new DateTime(2016, 12, 25), Rating = 8, OriginalLanguage = "English" },
                    new Film { OriginalTitle = "Dune", FrenchTitle = "Dune", Description = "Adaptation cinématographique du roman de science-fiction de Frank Herbert, à propos du fils d'une famille noble chargé de la protection de l'actif le plus précieux et de l'élément le plus vital de la galaxie.", Duration = 155, InternationalReleaseDate = new DateTime(2023, 9, 21), Rating = 7, OriginalLanguage = "English" },
                    new Film { OriginalTitle = "Django Unchained", FrenchTitle = "Django déchaîné", Description = "Deux ans avant la Guerre civile, un ancien esclave du nom de Django s'associe avec un chasseur de primes d'origine allemande qui l'a libéré: il accepte de traquer avec lui des criminels recherchés. En échange, il l'aidera à retrouver sa femme perdue depuis longtemps et esclave elle aussi.", Duration = 165, InternationalReleaseDate = new DateTime(2012, 12, 25), Rating = 7, OriginalLanguage = "English" }
                };

                _filmsCollection.InsertMany(films);
                Console.WriteLine("Données de films insérées avec succès.");
            }
        }

        public void SeedProjections()
        {
            if (!_projectionsCollection.Indexes.List().Any())
            {
                var projections = new List<Projection>
        {
            new Projection { DateHeureDebut = DateTime.Now.AddDays(1), 
                                Salle = _sallesCollection.Find(s => s.NumeroSalle == 1).FirstOrDefault(), 
                                Film = _filmsCollection.Find(f => f.OriginalTitle == "Inception").FirstOrDefault() },
            new Projection { DateHeureDebut = DateTime.Now.AddDays(2), 
                                Salle = _sallesCollection.Find(s => s.NumeroSalle == 2).FirstOrDefault(), 
                                Film = _filmsCollection.Find(f => f.OriginalTitle == "The Dark Knight").FirstOrDefault() },
        };

                _projectionsCollection.InsertMany(projections);
                Console.WriteLine("Données de projections insérées avec succès.");
            }
        }

        public void SeedSalles()
        {
            if(!_sallesCollection.Indexes.List().Any())
            {
                var salles = new List<Salle>
                {
                    new Salle { NumeroSalle = 1, NombrePlace = 100 },
                    new Salle { NumeroSalle = 2, NombrePlace = 120 },
                    new Salle { NumeroSalle = 3, NombrePlace = 80 },
                };

                _sallesCollection.InsertMany(salles);
                Console.WriteLine("Données des salles insérées avec succès.");
            }
        }

        public void SeedActeurs()
        {
            if (!_acteursCollection.Indexes.List().Any())
            {
                var acteurs = new List<Acteur>
                {
                    new Acteur { Nom = "DiCaprio", Prenom = "Leonardo" },
                    new Acteur { Nom = "Nolan", Prenom = "Christopher" },
                    new Acteur { Nom = "Cotillard", Prenom = "Marion" },
                    new Acteur { Nom = "Bale", Prenom = "Christian" },
                    new Acteur { Nom = "Ledger", Prenom = "Heath" },
                    new Acteur { Nom = "Freeman", Prenom = "Morgan" },
                    new Acteur { Nom = "McConaughey", Prenom = "Matthew" },
                    new Acteur { Nom = "Chastain", Prenom = "Jessica" },
                    new Acteur { Nom = "Hathaway", Prenom = "Anne" },
                    new Acteur { Nom = "Stone", Prenom = "Emma" },
                    new Acteur { Nom = "Legend", Prenom = "John" },
                    new Acteur { Nom = "Momoa", Prenom = "Jason" },
                    new Acteur { Nom = "Ferguson", Prenom = "Rebecca" },
                    new Acteur { Nom = "Foxx", Prenom = "Jamie" },
                    new Acteur { Nom = "Waltz", Prenom = "Christoph" },
                    new Acteur { Nom = "Washington", Prenom = "Kerry" },
                    new Acteur { Nom = "Jackson", Prenom = "Samuel Lee" },
                };
                _acteursCollection.InsertMany(acteurs);
                Console.WriteLine("Données d'acteur insérées avec succès.");
            }
                
        }

        public void SeedRealisateurs()
        {
            if (!_realisateursCollection.Indexes.List().Any())
            {
                var realisateurs = new List<Realisateur>
                {
                    new Realisateur { Nom = "Nolan", Prenom = "Christopher" },
                    new Realisateur { Nom = "Chazelle", Prenom = "Damien" },
                    new Realisateur { Nom = "Villeneuse", Prenom = "Denis" },
                    new Realisateur { Nom = "Tarantino", Prenom = "Quentin" },
                };
                _realisateursCollection.InsertMany(realisateurs);
                Console.WriteLine("Données de réalisateur insérées avec succès.");
            }
        }

        public void SeedCategories()
        {
            if (!_categoriesCollection.Indexes.List().Any())
            {
                var categories = new List<Categorie>
                {
                    new Categorie { Nom = "Science Fiction" },
                    new Categorie { Nom = "Action" },
                    new Categorie { Nom = "Drame" },
                    new Categorie { Nom = "Comédie" },
                    new Categorie { Nom = "Thriller" },
                    new Categorie { Nom = "Romance" },
                    new Categorie { Nom = "Western Spaghetti" },
                };
                _categoriesCollection.InsertMany(categories);
                Console.WriteLine("Données de catégorie insérées avec succès.");
            }
        }
    }
}
