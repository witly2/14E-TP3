using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data;

public class Acteur
{
    [BsonId] public ObjectId Id { get; }

    public string Nom { get; set; }
    public string Prenom { get; set; }
}