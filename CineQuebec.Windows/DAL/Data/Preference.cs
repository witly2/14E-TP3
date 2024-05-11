using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Preference
    {
        [BsonId]
        public ObjectId Id { get; private set; }
        public Abonne Abonne { get; private set; }
        public List<Realisateur> ListPreferenceRealisateur {  get; private set; }
        public List<Acteur> ListPreferenceActeur { get; private set; }
        public List<Categorie> ListPreferenceCategorie { get; private set; }

        public void SetAbonne(Abonne abonne)
        {
            Abonne = abonne;
        }

        public void SetListPreferenceRealisateur(List<Realisateur> newListRealisateur)
        {
            ListPreferenceRealisateur = newListRealisateur;
        }

        public void SetListPreferenceActeur(List<Acteur> newListActeur)
        {
            ListPreferenceActeur = newListActeur;
        }

        public void SetListPreferenceCategorie(List<Categorie> newListCategorie)
        {
            ListPreferenceCategorie = newListCategorie;
        }
    }
}
