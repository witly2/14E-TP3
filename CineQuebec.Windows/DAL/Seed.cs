using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;

namespace CineQuebec.Windows.DAL;

public class Seed
{
    private readonly IMongoCollection<Abonne> _abonnesCollection;
    private readonly IMongoCollection<Acteur> _acteursCollection;
    private readonly IMongoCollection<Categorie> _categoriesCollection;
    private readonly IMongoCollection<Film> _filmsCollection;
    private readonly IMongoCollection<Projection> _projectionsCollection;
    private readonly IMongoCollection<Realisateur> _realisateursCollection;
    private readonly IMongoCollection<Salle> _sallesCollection;

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
    ///     Liste de données pour les abonnés
    /// </summary>
    public async Task SeedAbonnes()
    {
        if (!_abonnesCollection.Indexes.List().Any())
        {
            var abonnes = new List<Abonne>
            {
                new() { Username = "John Doe", Email = "john.doe@example.com", DateAdhesion = DateTime.UtcNow },
                new() { Username = "Jane Smith", Email = "jane.smith@example.com", DateAdhesion = DateTime.UtcNow },
                new()
                {
                    Username = "Alice Johnson", Email = "alice.johnson@example.com", DateAdhesion = DateTime.UtcNow
                },
                new() { Username = "Bob Brown", Email = "bob.brown@example.com", DateAdhesion = DateTime.UtcNow },
                new()
                {
                    Username = "Michael Wilson", Email = "michael.wilson@example.com", DateAdhesion = DateTime.UtcNow
                },
                new() { Username = "Emily Davis", Email = "emily.davis@example.com", DateAdhesion = DateTime.UtcNow },
                new()
                {
                    Username = "Daniel Martinez", Email = "daniel.martinez@example.com", DateAdhesion = DateTime.UtcNow
                },
                new()
                {
                    Username = "Sarah Anderson", Email = "sarah.anderson@example.com", DateAdhesion = DateTime.UtcNow
                },
                new() { Username = "David Taylor", Email = "david.taylor@example.com", DateAdhesion = DateTime.UtcNow },
                new()
                {
                    Username = "Olivia Thomas", Email = "olivia.thomas@example.com", DateAdhesion = DateTime.UtcNow
                },
                new()
                {
                    Username = "admin", Email = "admin@test.com", DateAdhesion = DateTime.UtcNow,
                    Password = Convert.FromBase64String("QfWXKJ2prwJ4UOw/dvP22g=="),
                    Salt = Convert.FromBase64String("YlW4gWxsBG3IYq/sKPlnpA=="),
                    IsAdmin = true
                }
            };

            _abonnesCollection.InsertMany(abonnes);
            Console.WriteLine("Données d'abonnés insérées avec succès.");
        }
    }

    /// <summary>
    ///     Liste de données pour les films
    /// </summary>
    public async Task SeedFilms()
    {
        if (!_filmsCollection.Indexes.List().Any())
        {
            var films = new List<Film>
            {
                new("Inception", "Inception",
                    "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148,
                    new DateTime(2010, 7, 16), 8),
                new("The Dark Knight", "Le Chevalier Noir",
                    "Lorsque la menace connue sous le nom du Joker sème le chaos parmi les habitants de Gotham, Batman doit accepter l'une des plus grandes épreuves psychologiques et physiques de sa capacité à lutter contre l'injustice.",
                    152, new DateTime(2008, 7, 18), 9),
                new("Interstellar", "Interstellaire",
                    "Une équipe d'explorateurs voyage à travers un trou de ver dans l'espace dans le but de garantir la survie de l'humanité.",
                    169, new DateTime(2014, 11, 7), 8),
                new("La La Land", "La La Land",
                    "En naviguant dans leur carrière à Los Angeles, un pianiste et une actrice tombent amoureux tout en tentant de concilier leurs aspirations pour l'avenir.",
                    128, new DateTime(2016, 12, 25), 8),
                new("Dune", "Dune",
                    "Adaptation cinématographique du roman de science-fiction de Frank Herbert, à propos du fils d'une famille noble chargé de la protection de l'actif le plus précieux et de l'élément le plus vital de la galaxie.",
                    155, new DateTime(2023, 9, 21), 7),
                new("Django Unchained", "Django déchaîné",
                    "Deux ans avant la Guerre civile, un ancien esclave du nom de Django s'associe avec un chasseur de primes d'origine allemande qui l'a libéré: il accepte de traquer avec lui des criminels recherchés. En échange, il l'aidera à retrouver sa femme perdue depuis longtemps et esclave elle aussi.",
                    165, new DateTime(2012, 12, 25), 7)
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
                new(new DateTime(2024, 3, 10, 20, 0, 0),
                    _sallesCollection.Find(s => s.NumeroSalle == 1).FirstOrDefault(),
                    _filmsCollection.Find(f => f.OriginalTitle == "Inception").FirstOrDefault()),
                new(new DateTime(2024, 5, 6, 19, 0, 0),
                    _sallesCollection.Find(s => s.NumeroSalle == 2).FirstOrDefault(),
                    _filmsCollection.Find(f => f.OriginalTitle == "The Dark Knight").FirstOrDefault())
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
                new(1, 100),
                new(2, 120),
                new(3, 80)
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
                new() { Nom = "DiCaprio", Prenom = "Leonardo" },
                new() { Nom = "Nolan", Prenom = "Christopher" },
                new() { Nom = "Cotillard", Prenom = "Marion" },
                new() { Nom = "Bale", Prenom = "Christian" },
                new() { Nom = "Ledger", Prenom = "Heath" },
                new() { Nom = "Freeman", Prenom = "Morgan" },
                new() { Nom = "McConaughey", Prenom = "Matthew" },
                new() { Nom = "Chastain", Prenom = "Jessica" },
                new() { Nom = "Hathaway", Prenom = "Anne" },
                new() { Nom = "Stone", Prenom = "Emma" },
                new() { Nom = "Legend", Prenom = "John" },
                new() { Nom = "Momoa", Prenom = "Jason" },
                new() { Nom = "Ferguson", Prenom = "Rebecca" },
                new() { Nom = "Foxx", Prenom = "Jamie" },
                new() { Nom = "Waltz", Prenom = "Christoph" },
                new() { Nom = "Washington", Prenom = "Kerry" },
                new() { Nom = "Jackson", Prenom = "Samuel Lee" }
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
                new() { Nom = "Nolan", Prenom = "Christopher" },
                new() { Nom = "Chazelle", Prenom = "Damien" },
                new() { Nom = "Villeneuse", Prenom = "Denis" },
                new() { Nom = "Tarantino", Prenom = "Quentin" }
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
                new() { Nom = "Science Fiction" },
                new() { Nom = "Action" },
                new() { Nom = "Drame" },
                new() { Nom = "Comédie" },
                new() { Nom = "Thriller" },
                new() { Nom = "Romance" },
                new() { Nom = "Western Spaghetti" }
            };
            _categoriesCollection.InsertMany(categories);
            Console.WriteLine("Données de catégorie insérées avec succès.");
        }
    }
}