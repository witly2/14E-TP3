using System.Collections.ObjectModel;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Films;
using System.Linq;


namespace CineQuebec.Windows.ViewModels;

public class AccueilViewModel:Utilities.ViewModelBase
{
    private readonly FilmRepository _filmRepository;
    private readonly DatabaseGestion _db;
    private ObservableCollection<Film> _films;
    
    public ObservableCollection<Film>? Films
    {
        get { return _films; }
        set
        {
            _films = value;
            OnPropertyChanged();
        }
    }

    public AccueilViewModel()
    {
        _db = new DatabaseGestion();
        _filmRepository = new FilmRepository(_db);
        Films = new ObservableCollection<Film>(_filmRepository.GetFilms().Result);
    }
}