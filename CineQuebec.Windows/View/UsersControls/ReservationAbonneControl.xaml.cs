using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.ViewModels.AbonneViewModel;

namespace CineQuebec.Windows.View.UsersControls;

public partial class ReservationAbonneControl : UserControl
{
    public ReservationAbonneControl(Abonne abonne)
    {
        InitializeComponent();
        var ReservationAbonneViewModel = new AbonneReservationViewModel(abonne);
        DataContext = ReservationAbonneViewModel.Reservations;
    }



    private void Btn_Update_Reservation(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var reservation = (Reservation)button.DataContext;
        var updateReservation = new UpdateReservationForm(reservation);
        updateReservation.ShowDialog();
    }
}