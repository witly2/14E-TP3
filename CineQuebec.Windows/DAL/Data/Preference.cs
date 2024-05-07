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
        public ObjectId AbonneId { get; private set; }
        public List<Realisateur> ListPreferenceRealisateur {  get; private set; } // max 5
        public List<Acteur> ListPreferenceActeur { get; private set; } // max 5
        public List<Categorie> ListPreferenceCategorie { get; private set; } // max 3


    }
}
