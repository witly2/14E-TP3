using System.Collections.ObjectModel;
using System.Windows;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Projections;
using MongoDB.Driver;

namespace CineQuebec.Windows.ViewModels;

public class ProjectionViewModel:Utilities.ViewModelBase
{
    private readonly DatabaseGestion _db;
    private ObservableCollection<Projection> _projection;
    private readonly ProjectionRepository _projectionRepository;



    public ProjectionViewModel(Film film)

    {
        _db = new DatabaseGestion();
        _projectionRepository = new ProjectionRepository(_db);
        LoadProjectionsAsync(film);
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