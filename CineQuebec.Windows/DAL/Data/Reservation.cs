using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data;

public class Reservation
{
    public Reservation(Abonne abonne, Projection projection)
    {
        Abonne = abonne;
        Projection = projection;
    }

    [BsonId] public ObjectId Id { get; }
    
    public Abonne Abonne { get; set; }
    public Projection Projection { get; set; }
    
    
}