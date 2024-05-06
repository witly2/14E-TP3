using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data;

public class Categorie
{
    [BsonId] public ObjectId Id { get; }

    public string Nom { get; set; }
}