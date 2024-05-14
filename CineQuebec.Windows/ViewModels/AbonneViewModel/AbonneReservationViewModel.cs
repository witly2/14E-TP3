using System.Collections.ObjectModel;
using CineQuebec.Windows.BLL.Services.Reservations;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Reservation;
using CineQuebec.Windows.Utilities;

namespace CineQuebec.Windows.ViewModels.AbonneViewModel;

public class AbonneReservationViewModel:ViewModelBase
{
    private readonly DatabaseGestion _db;
    private readonly ReservationService _reservationService;
    private List<Reservation> _reservations;

    public List<Reservation> Reservations
    {
        get
        {
            return  _reservations;
        }
        set
        {
            _reservations = value;
            OnPropertyChanged();
        }
    }

    public AbonneReservationViewModel(Abonne abonne)
    {
        _db = new DatabaseGestion();
        _reservationService = new ReservationService(new ReservationRepository(_db));
        LoadReservations(abonne);
    }
    
    private  void LoadReservations(Abonne abonne)
    {
        Reservations = _reservationService.GetReservationsAbonne(abonne);
    }
 
}