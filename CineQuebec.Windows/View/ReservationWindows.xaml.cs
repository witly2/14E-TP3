using System.Windows;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.BLL.Services.Reservations;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Repositories.Projections;
using CineQuebec.Windows.DAL.Repositories.Reservation;
using CineQuebec.Windows.ViewModels;

namespace CineQuebec.Windows.View;

public partial class ReservationWindows : Window
{
    private readonly Film _film;
    private readonly Abonne _abonne;
    private readonly IProjectionService _projectionService;
    private readonly ReservationService _reservationService;
    public ReservationWindows(Abonne abonne, Film film)
    {
        
        InitializeComponent();
        
        _abonne = abonne;
        _film = film;
        titreFilm.Text = film.FrenchTitle;
        DatabaseGestion db = new DatabaseGestion();
        _reservationService = new ReservationService(new ReservationRepository(db));
        var projectionViewModel = new ProjectionViewModel(film);

        DataContext = projectionViewModel;

        
    }

    private void Btn_Reservation(object sender, RoutedEventArgs e)
    {
        
        try
        {
             // var projections = _projectionService.GetProjections(_film);
             
             
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private void BtnAnuler(object sender, RoutedEventArgs e)
    {
        Close();
    }
}