using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Recompense
    {
        [BsonId]
        public ObjectId Id { get; private set; }
        public List<Abonne> Abonne { get; private set; }
        public TypeRecompense Type { get; private set; }
        public Projection Projection { get; private set; }
        public int NombrePlace {  get; private set; }
        public int NombrePlaceRestante { get; private set; }

        public Recompense(List<Abonne> abonne, TypeRecompense type, Projection projection, int nombrePlace)
        {
            Abonne = abonne;
            Type = type;
            Projection = projection;
            NombrePlace = nombrePlace;
        }

        public void SetAbonne(List<Abonne> abonne)
        {
            Abonne = abonne;
        }

        public void SetTypeRecompense(TypeRecompense type)
        {
            Type = type;
        }

        public void SetProjection(Projection projection)
        {
            Projection = projection;
        }

        public void SetNombrePlaceRestante(int nombrePlaceRestante)
        {
            NombrePlaceRestante = nombrePlaceRestante;
        }
    }
}
