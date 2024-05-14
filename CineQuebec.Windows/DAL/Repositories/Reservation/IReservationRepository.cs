using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.Repositories.Reservation;

public interface IReservationRepository
{
    Task AddReservation(Data.Reservation reservation);
    Task<List<Data.Reservation>> GetReservationCountByAbonneId(Abonne abonne);
}