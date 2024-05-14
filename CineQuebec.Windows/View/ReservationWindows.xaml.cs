using System.Windows;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.BLL.Services.Reservations;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
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
        _abonne = abonne;
        titreFilm.Text = film.FrenchTitle;
        DatabaseGestion db = new DatabaseGestion();
        _reservationService = new ReservationService(new ReservationRepository(db));
        var projectionViewModel = new ProjectionViewModel(film);

        DataContext = projectionViewModel;

        
    }

    private async void Btn_Reservation(object sender, RoutedEventArgs e)
    {
        ProjectionViewModel? viewModel = DataContext as ProjectionViewModel;

        if (viewModel != null)
        {
            Projection? selectedProjection = viewModel.SelectedProjection;

            if (selectedProjection != null && ushort.TryParse(nbrePlaceTxt.Text, out var nbrePlace) && nbrePlace > 0 && nbrePlace <= selectedProjection.Salle.NombrePlace )
            {
                Reservation reservation = new Reservation()
                {
                    Abonne = _abonne,
                    Projection = selectedProjection,
                    NombreBillets = nbrePlace
                };
                try
                {
                    
                  await  _reservationService.AddReservation(reservation);
                    DialogResult = true;
                    Close();


                }
                catch (ReservationExisteException)
                {
                  await  _reservationService.UpdateReservation(reservation, true);
                  DialogResult = true;
                  Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
            else
            {
                MessageBox.Show($"Veuillez vérifier les informations saisies. Le nombre de place doit être compris entre 1 et {selectedProjection.Salle.NombrePlace}");
            }
        }

       
    }

    private void BtnAnuler(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}