using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.Utilities;

namespace CineQuebec.Windows.ViewModels;

public class AccueilFilmViewModel : ViewModelBase
{
    private readonly DatabaseGestion _db;
    private readonly FilmRepository _filmRepository;
    private readonly ContentControl _mainContentControl;
    private ObservableCollection<Film> _films;

    public AccueilFilmViewModel()
    {
        _db = new DatabaseGestion();
        _filmRepository = new FilmRepository(_db);
        LoadFilmsAsync();
    }

    public ObservableCollection<Film>? Films
    {
        get => _films;
        set
        {
            _films = value;
            OnPropertyChanged();
        }
    }

    public ICommand YourCommand { get; }

    public async Task LoadFilmsAsync()
    {
        try
        {
            IEnumerable<Film> films = await _filmRepository.GetFilms();
            Films = new ObservableCollection<Film>(films);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading films: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}