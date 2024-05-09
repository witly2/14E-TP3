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

    }
}
