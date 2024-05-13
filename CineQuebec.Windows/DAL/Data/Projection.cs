using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Projection
    {
        [BsonId]
        public ObjectId Id { get; private set; }
        public DateTime DateHeureDebut { get; set; }
        public Salle Salle { get; set; }
        public Film Film { get; set; }

        public DateTime DateHeureFin { get; private set; }
        public Projection(DateTime pDateHeureDebut, Salle pSalle, Film pFilm) 
        { 
            DateHeureDebut = pDateHeureDebut;
            Salle = pSalle;
            Film = pFilm;
            DateHeureFin = DateHeureDebut.AddMinutes(pFilm.Duration);   
        }
        
        public string GetDateHeureDebut()
        {
            return DateHeureDebut.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
