using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

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
        private readonly IMongoCollection<Admin> _adminsCollection;
        private readonly IMongoCollection<Person> _personsCollection;

        public Seed(IMongoDatabase database)
        {
            _abonnesCollection = database.GetCollection<Abonne>("Abonnes");
            _filmsCollection = database.GetCollection<Film>("Films");
            _acteursCollection = database.GetCollection<Acteur>("Acteurs");
            _realisateursCollection = database.GetCollection<Realisateur>("Realisateurs");
            _categoriesCollection = database.GetCollection<Categorie>("Categories");
            _sallesCollection = database.GetCollection<Salle>("Salles");
            _projectionsCollection = database.GetCollection<Projection>("Projections");
            _adminsCollection = database.GetCollection<Admin>("Admins");
            _personsCollection = database.GetCollection<Person>("Persons");
        }


        /// <summary>
        ///  Liste de données pour les abonnés
        /// </summary>
        public async Task SeedAbonnes()
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
        public async Task SeedFilms()
        {
            if (!_filmsCollection.Indexes.List().Any())
            {
                var films = new List<Film>
                {
                    new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8),
                    new Film("The Dark Knight", "Le Chevalier Noir", "Lorsque la menace connue sous le nom du Joker sème le chaos parmi les habitants de Gotham, Batman doit accepter l'une des plus grandes épreuves psychologiques et physiques de sa capacité à lutter contre l'injustice.", 152, new DateTime(2008, 7, 18), 9),
                    new Film("Interstellar", "Interstellaire", "Une équipe d'explorateurs voyage à travers un trou de ver dans l'espace dans le but de garantir la survie de l'humanité.", 169, new DateTime(2014, 11, 7), 8),
                    new Film("La La Land", "La La Land", "En naviguant dans leur carrière à Los Angeles, un pianiste et une actrice tombent amoureux tout en tentant de concilier leurs aspirations pour l'avenir.", 128, new DateTime(2016, 12, 25), 8),
                    new Film("Dune", "Dune", "Adaptation cinématographique du roman de science-fiction de Frank Herbert, à propos du fils d'une famille noble chargé de la protection de l'actif le plus précieux et de l'élément le plus vital de la galaxie.", 155, new DateTime(2023, 9, 21), 7),
                    new Film("Django Unchained", "Django déchaîné", "Deux ans avant la Guerre civile, un ancien esclave du nom de Django s'associe avec un chasseur de primes d'origine allemande qui l'a libéré: il accepte de traquer avec lui des criminels recherchés. En échange, il l'aidera à retrouver sa femme perdue depuis longtemps et esclave elle aussi.", 165, new DateTime(2012, 12, 25), 7)
                };

                _filmsCollection.InsertMany(films);
                Console.WriteLine("Données de films insérées avec succès.");
            }
        }

        public async Task SeedProjections()
        {
            if (!_projectionsCollection.Indexes.List().Any())
            {
                var projections = new List<Projection>
                {
                    new Projection(new DateTime(2024, 3, 10, 20, 0, 0),
                                _sallesCollection.Find(s => s.NumeroSalle == 1).FirstOrDefault(),
                                _filmsCollection.Find(f => f.OriginalTitle == "Inception").FirstOrDefault()),
                    new Projection(new DateTime(2024, 5, 6, 19, 0, 0),
                                _sallesCollection.Find(s => s.NumeroSalle == 2).FirstOrDefault(),
                                _filmsCollection.Find(f => f.OriginalTitle == "The Dark Knight").FirstOrDefault()),
                };

                _projectionsCollection.InsertMany(projections);
                Console.WriteLine("Données de projections insérées avec succès.");
            }
        }

        public async Task SeedSalles()
        {
            if (!_sallesCollection.Indexes.List().Any())
            {
                var salles = new List<Salle>
                {
                    new Salle (1, 100),
                    new Salle (2, 120),
                    new Salle (3, 80),
                };

                _sallesCollection.InsertMany(salles);
                Console.WriteLine("Données des salles insérées avec succès.");
            }
        }

        public async Task SeedActeurs()
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

        public async Task SeedRealisateurs()
        {
            if (!_realisateursCollection.Indexes.List().Any())
            {
                var realisateurs = new List<Realisateur>
                {
                    new Realisateur("Nolan", "Christopher"),
                    new Realisateur ("Chazelle", "Damien"),
                    new Realisateur ("Villeneuse", "Denis"),
                    new Realisateur ("Tarantino", "Quentin"),
                };
                _realisateursCollection.InsertMany(realisateurs);
                Console.WriteLine("Données de réalisateur insérées avec succès.");
            }
        }

        public async Task SeedCategories()
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

        public class PasswordHasher
        {
            public static byte[] HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return hashedBytes;
                }
            }
        }

        public async Task SeedAdmins()
        {
            if (!_adminsCollection.Indexes.List().Any())
            {
                byte[] hashedPassword = PasswordHasher.HashPassword("Admin123!");
                var adminPerson = new Person
                {
                    Nom = "Admin",
                    Email = "admin@mail.com",
                    Username = "Admin",
                    Password = hashedPassword
                };
                await _personsCollection.InsertOneAsync(adminPerson);

                var admin = new Admin();
                admin.SetId(adminPerson.Id);
                await _adminsCollection.InsertOneAsync(admin);
                Console.WriteLine("Données d'administrateur insérées avec succès.");
            }
        }
    }
}
