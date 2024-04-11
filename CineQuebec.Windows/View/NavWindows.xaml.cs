
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL;
using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Data;


namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour NavWindows.xaml
    /// </summary>
    public partial class NavWindows : Window
    {
        private readonly FilmService _filmService;
        private readonly AbonneService _abonneService;
        private readonly AdminHomeControl _adminHomeControl;
        public NavWindows(Abonne abonne)
        {
            DatabaseGestion db = new DatabaseGestion();
            _filmService = new FilmService(new FilmRepository(db));
            _abonneService = new AbonneService(new AbonneRepsitory(db));
            _adminHomeControl = new AdminHomeControl(abonne);

            InitializeComponent();
            AdminName.Text = abonne.Username;
            mainContentControl.Content = _adminHomeControl;

        }


        /// <summary>
        /// Bouton navigation Abonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new UsersControl(_abonneService);

        }

        /// <summary>
        /// Bouton Navigation Film
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Films_Click(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new FilmsControl(_filmService);
        }
        /// <summary>
        /// Déconnexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        /// <summary>
        /// Boutton navigation Accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = _adminHomeControl;
        }
    }
}
