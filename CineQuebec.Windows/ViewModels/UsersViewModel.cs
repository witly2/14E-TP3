using System.Collections.ObjectModel;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.Utilities;

namespace CineQuebec.Windows.ViewModels;

public class UsersViewModel : ViewModelBase
{
    private readonly DatabaseGestion UsersVm_db;
    private ObservableCollection<Abonne> _abonnes;

    public UsersViewModel()
    {
        UsersVm_db = new DatabaseGestion();
        Abonnes = new ObservableCollection<Abonne>(UsersVm_db.ReadAbonnes());
    }

    public ObservableCollection<Abonne>? Abonnes
    {
        get => _abonnes;
        set
        {
            _abonnes = value;
            OnPropertyChanged();
        }
    }
}