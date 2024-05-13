using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View;

public partial class DetailFilmControl : UserControl
{
    private Abonne _abonne;
    private readonly Film _film;
    public DetailFilmControl(Film film, Abonne abonne=null)
    {
        InitializeComponent();
        this.DataContext = film;
        _film = film;
        _abonne = abonne;
    }

    private void ButtonREserver_Place_Click(object sender, RoutedEventArgs e)
    {
        if (_abonne is null)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ConnexionControl(_film);
        }
        else
        {
            ReservationWindows reservationWindows = new ReservationWindows( _abonne,_film);
            reservationWindows.ShowDialog();
        }
       
    }
}