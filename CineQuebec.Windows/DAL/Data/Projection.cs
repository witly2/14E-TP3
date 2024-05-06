using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data;

public class Projection
{
    public Projection(DateTime pDateHeureDebut, Salle pSalle, Film pFilm)
    {
        DateHeureDebut = pDateHeureDebut;
        Salle = pSalle;
        Film = pFilm;
    }

    [BsonId] public ObjectId Id { get; }

    public DateTime DateHeureDebut { get; set; }
    public Salle Salle { get; set; }
    public Film Film { get; set; }
    
}