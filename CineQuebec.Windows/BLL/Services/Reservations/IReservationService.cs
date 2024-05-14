using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.BLL.Services.Reservations;

public interface IReservationService
{
    Task AddReservation(Reservation reservation);
    List<Reservation> GetReservationsAbonne(Abonne abonne);
    Task UpdateReservation(Reservation reservation, bool isCreation = false);
}