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
        
        if (_reservationsCollection.Find(x => x.Projection.Id == reservation.Projection.Id && x.Abonne == reservation.Abonne).Any())
        {
            throw new ReservationExisteException("Vous avez deja une reservation pour cette projection");
        }
        await _reservationsCollection.InsertOneAsync(reservation);
    }
    
    public async Task UpdateReservation(Data.Reservation reservation, bool isCreation = false)
    {
        Data.Reservation res = null;
        if (isCreation)
        {
             res = _reservationsCollection.Find(x => x.Projection.DateHeureDebut == reservation.Projection.DateHeureDebut
            && x.Projection.Salle == reservation.Projection.Salle) .FirstOrDefault();
             reservation.NombreBillets+=res.NombreBillets;
             reservation.SetId(res.Id);
        }
        else
        {
            res = _reservationsCollection.Find(x => x.Id == reservation.Id).FirstOrDefault();
        }
       
        if(res is null )
        {
            throw new NullReferenceException("La reservation n'existe pas");
        }
        await _reservationsCollection.ReplaceOneAsync(x => x.Id == res.Id, reservation);
    }

    public List<Data.Reservation> GetReservationsAbonne(Abonne abonne)
    {
      return _reservationsCollection.Find(x => x.Abonne.Id == abonne.Id).ToList();
    }
}