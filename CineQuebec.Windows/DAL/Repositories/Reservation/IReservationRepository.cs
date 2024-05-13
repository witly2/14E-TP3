namespace CineQuebec.Windows.DAL.Repositories.Reservation;

public interface IReservationRepository
{
    Task AddReservation(Data.Reservation reservation);
}