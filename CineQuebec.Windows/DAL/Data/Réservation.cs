using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data;

public class Réservation
{
    [BsonId]
    public ObjectId Id { get; private set; }
    
    public Projection Projection { get; set; }
    
    public ushort NombreBillets { get; set; }
}