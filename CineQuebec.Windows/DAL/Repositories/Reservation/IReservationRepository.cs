using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.Repositories.Reservation;

public interface IReservationRepository
{
    Task AddReservation(Data.Reservation reservation);
    Task UpdateReservation(Data.Reservation reservation, bool isCreation = false);
    List<Data.Reservation> GetReservationsAbonne(Abonne abonne);
}