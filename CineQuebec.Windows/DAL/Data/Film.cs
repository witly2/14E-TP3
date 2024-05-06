using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data;

public class Film
{
    public Film(string pOriginalTitle, string pFrenchTitle, string pDescription, ushort pDuration,
        DateTime pInternationalReleaseDate, ushort pRating)
    {
        OriginalTitle = pOriginalTitle;
        FrenchTitle = pFrenchTitle;
        Description = pDescription;
        Duration = pDuration;
        InternationalReleaseDate = pInternationalReleaseDate;
        Rating = pRating;
    }

    [BsonId] public ObjectId Id { get; }

    [Required(ErrorMessage = "Le titre original est requis.")]
    public string OriginalTitle { get; set; }

    [Required(ErrorMessage = "Le titre français est requis.")]
    public string FrenchTitle { get; set; }

    public string Description { get; set; }

    [Range(1, ushort.MaxValue, ErrorMessage = "La durée doit être supérieure à zéro.")]
    public ushort Duration { get; set; }

    public DateTime InternationalReleaseDate { get; set; }
    public ushort? Rating { get; set; }
}