using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data;

public class Abonne
{
    [BsonId] public ObjectId Id { get; }

    public string Username { get; set; }
    public DateTime DateAdhesion { get; set; }
    public string Email { get; set; }
    public byte[] Password { get; set; }
    public byte[] Salt { get; set; }
    public bool IsAdmin { get; set; } = false;
}