﻿using CineQuebec.Windows.DAL.Data;
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

    public async Task<List<Reservation>> GetReservationCountByAbonneId(Abonne abonne)
    {
        var reservations = await _reservationRepository.GetReservationCountByAbonneId(abonne);
        return reservations;
    }
}