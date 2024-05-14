using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Reservation;

namespace CineQuebec.Windows.BLL.Services.Reservations;

public class ReservationService:IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }
   
    public async Task AddReservation(Reservation reservation)
    {
       await _reservationRepository.AddReservation(reservation);
    }

    public List<Reservation> GetReservationsAbonne(Abonne abonne)
    {
       return _reservationRepository.GetReservationsAbonne(abonne);
    }



    public async Task UpdateReservation(Reservation reservation, bool isCreation = false)
    {
       await _reservationRepository.UpdateReservation(reservation, isCreation);
    }
}