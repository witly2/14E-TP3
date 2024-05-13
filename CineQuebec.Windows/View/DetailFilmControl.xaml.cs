using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.View.AbonneView;

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
            if ((bool)reservationWindows.ShowDialog())
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is NavWindowsAbonneView navWindowsAbonne)
                    {
                        navWindowsAbonne.mainContentControl.Content = new AccueilFilmControl(_abonne);
                        MessageBox.Show("Le film a été réservé avec succès!");
                        break;
                    }
                }
            }
        }
       
    }
}