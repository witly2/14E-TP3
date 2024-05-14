using System.Windows;
using CineQuebec.Windows.BLL.Services.Reservations;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Reservation;

namespace CineQuebec.Windows.View;

public partial class UpdateReservationForm : Window
{
    private readonly Reservation _reservation;
    private readonly ReservationService _reservationService;
    private DatabaseGestion _db;
    public UpdateReservationForm(Reservation reservation)
    {
        InitializeComponent();
        DataContext = reservation;
        _db = new DatabaseGestion();
        _reservationService = new ReservationService(new ReservationRepository(_db));
    }

    private async void Btn_Update(object sender, RoutedEventArgs e)
    {
        try
        {
            await _reservationService.UpdateReservation(DataContext as Reservation);
            DialogResult = true;
            Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show("Impossible de mettre à jour la réservation : " + exception.Message);
            throw;
        }
    }

    private void BtnAnuler(object sender, RoutedEventArgs e)
    {
        DialogResult= false;
       Close();
       
    }
}