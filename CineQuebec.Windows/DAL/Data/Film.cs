using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Film
    {
        [BsonId]
        public ObjectId Id { get; private set; }
        [Required(ErrorMessage = "Le titre original est requis.")]
        public string OriginalTitle { get; set; }
        [Required(ErrorMessage = "Le titre français est requis.")]
        public string FrenchTitle { get; set; }
        public string Description { get; set; }
        [Range(1, ushort.MaxValue, ErrorMessage = "La durée doit être supérieure à zéro.")]
        public ushort Duration { get; set; }

        public string DureeEnHeure
        {
            get
            {
                return DurationToString();
            }
        }
        
        public string Acteurs
        {
            get
            {
                return string.Join(", ", ListActeur.Select(a => a.Nom));
            }
        }
        
        public string Realisateurs
        {
            get
            {
                return string.Join(", ", ListRealisateur.Select(r => r.Nom));
            }
        }
        
        public string Categories
        {
            get
            {
                return string.Join(", ", ListCategorie.Select(c => c.Nom));
            }
        }
        

        public DateTime InternationalReleaseDate { get; set; }
        public ushort? Rating { get; set; }
        public List<Realisateur> ListRealisateur { get; private set; } = new List<Realisateur>();
        public List<Acteur> ListActeur { get; private set; } = new List<Acteur>();
        public List<Categorie> ListCategorie { get; private set; } = new List<Categorie>();

        public Film(string pOriginalTitle, string pFrenchTitle, string pDescription, ushort pDuration, DateTime pInternationalReleaseDate, ushort pRating)
        {
            OriginalTitle = pOriginalTitle;
            FrenchTitle = pFrenchTitle;
            Description = pDescription;
            Duration = pDuration;
            InternationalReleaseDate = pInternationalReleaseDate;
            Rating = pRating;
            
            var acteurs = new List<Acteur>
            {
                new Acteur { Nom = "DiCaprio", Prenom = "Leonardo" },
                new Acteur { Nom = "Nolan", Prenom = "Christopher" },
                new Acteur { Nom = "Cotillard", Prenom = "Marion" },
                new Acteur { Nom = "Bale", Prenom = "Christian" },
                new Acteur { Nom = "Ledger", Prenom = "Heath" }

            };
            
            ListActeur = acteurs;
            
            var realisateurs = new List<Realisateur>
            {
                new Realisateur("Nolan", "Christopher"),
                new Realisateur ("Chazelle", "Damien"),
                new Realisateur ("Villeneuse", "Denis"),
                new Realisateur ("Tarantino", "Quentin"),
            };
            
            ListRealisateur= realisateurs;
            
            var categories = new List<Categorie>
            {
                new Categorie { Nom = "Science Fiction" },
                new Categorie { Nom = "Action" },
                new Categorie { Nom = "Drame" },
               
            };
            
            ListCategorie = categories;
        }

        public void SetListRealisateur(List<Realisateur> realisateurs)
        {
            ListRealisateur = realisateurs;
        }

        public void SetListActeur(List<Acteur> acteurs)
        {
            ListActeur = acteurs;
        }

        public void SetListCategorie(List<Categorie> categories)
        {
            ListCategorie = categories;
        }
        
        public string DurationToString()
        {
            return $"{Duration / 60}h{Duration % 60}";
        }

    }
}
