using CineQuebec.Windows.DAL.Data;
using MongoDB.Driver;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;

namespace CineQuebec.Windows.DAL.Repositories.Reservation;

public class ReservationRepository:IReservationRepository
{
    private readonly DatabaseGestion _databaseGestion;
    private readonly IMongoCollection<Projection> _projectionsCollection;
    private readonly IMongoCollection<Data.Reservation> _reservationsCollection;
    
    public ReservationRepository(DatabaseGestion databaseGestion)
    {
        _databaseGestion = databaseGestion;
        _projectionsCollection = _databaseGestion.GetProjectionsCollection().Result;
        _reservationsCollection = _databaseGestion.GetReservationCollection().Result;
    }
    
    public async Task AddReservation(Data.Reservation reservation)
    {
        if (_reservationsCollection.Find(x => x.Projection == reservation.Projection && x.Abonne == reservation.Abonne).Any())
        {
            throw new ReservationExisteException("Vous avez deja une reservation pour cette projection");
        }
        await _reservationsCollection.InsertOneAsync(reservation);
    }
}