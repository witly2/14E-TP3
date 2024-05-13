using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Salle
    {
        [BsonId]
        public ObjectId Id { get; private set; }
        public int NumeroSalle { get; set; }
        public int NombrePlace { get; set; }
        
        public string GetNumeroSalle
        {
            get
            {
                return NumeroSalle.ToString();
            }
        }

        public Salle(int pNumeroSalle, int pNombrePlace) 
        {
            NumeroSalle = pNumeroSalle;
            NombrePlace = pNombrePlace;
        }
    }
}
