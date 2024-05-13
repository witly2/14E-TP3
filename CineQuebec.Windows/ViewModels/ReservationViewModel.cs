using System.Collections.ObjectModel;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.ViewModels;

public class ReservationViewModel:Utilities.ViewModelBase
{
    private readonly DatabaseGestion _db;
    private ObservableCollection<Reservation> _reservations;
    
    public ObservableCollection<Reservation>? Reservations
    {
        get { return _reservations; }
        set
        {
            _reservations = value;
            OnPropertyChanged();
        }
        
    }

    public ReservationViewModel()
    {
        _db = new DatabaseGestion();
        //Reservations = new ObservableCollection<Reservation>(_db.Get());
    }
}