using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Projections;
using MongoDB.Driver;
using Page_Navigation_App.Utilities;

namespace CineQuebec.Windows.ViewModels;

public class ProjectionViewModel:Utilities.ViewModelBase
{
    private readonly DatabaseGestion _db;
    private ObservableCollection<Projection> _projection;
    private readonly ProjectionRepository _projectionRepository;
    private Projection _selectedProjection;

    public ICommand SelectDateCommand { get; }
    public ProjectionViewModel(Film film)

    {
        _db = new DatabaseGestion();
        _projectionRepository = new ProjectionRepository(_db);
        LoadProjectionsAsync(film);
        SelectDateCommand = new RelayCommand(SelectDate);
    }
    
    public ObservableCollection<Projection> Projections
    {
        get=>_projection;
        set
        {
            _projection = value;
            OnPropertyChanged();
        }
    }
    
    private int _nombreDePlacesDisponibles;
    public int NombreDePlacesDisponibles
    {
        get { return _nombreDePlacesDisponibles; }
        set
        {
            if (_nombreDePlacesDisponibles != value)
            {
                _nombreDePlacesDisponibles = value;
                OnPropertyChanged();
            }
        }
    }
    
    private int _numeroDeSalles;
    public int NumeroDeSalles
    {
        get { return _numeroDeSalles; }
        set
        {
            if (_numeroDeSalles != value)
            {
                _numeroDeSalles = value;
                OnPropertyChanged();
            }
        }
    }

    public Projection SelectedProjection
    {
        get { return _selectedProjection; }
        set
        {
            if (_selectedProjection != value)
            {
                _selectedProjection = value;
                OnPropertyChanged(nameof(SelectedProjection));
            }
        }
    }

    private void SelectDate(object parameter)
    {
        if (parameter is Projection selectedDate)
        {
  
            SelectedProjection = Projections.FirstOrDefault(p => p.Id == selectedDate.Id);
            if (SelectedProjection != null)
            {
                NombreDePlacesDisponibles = SelectedProjection.Salle.NombrePlace;
                NumeroDeSalles = SelectedProjection.Salle.NumeroSalle;
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public async Task LoadProjectionsAsync(Film film)
    {
        try
        {
            IEnumerable<Projection> projections = await _projectionRepository.GetProjectionsForFilm(film);
            Projections = new ObservableCollection<Projection>(projections);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading projections: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}