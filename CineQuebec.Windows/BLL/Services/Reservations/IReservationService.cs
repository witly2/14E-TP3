using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.BLL.Services.Reservations;

public interface IReservationService
{
    Task AddReservation(Reservation reservation);
    Task<List<Reservation>> GetReservationCountByAbonneId(Abonne abonne);
}