using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data;

public class Salle
{
    public Salle(int pNumeroSalle, int pNombrePlace)
    {
        NumeroSalle = pNumeroSalle;
        NombrePlace = pNombrePlace;
    }

    [BsonId] public ObjectId Id { get; }

    public int NumeroSalle { get; set; }
    public int NombrePlace { get; set; }
}