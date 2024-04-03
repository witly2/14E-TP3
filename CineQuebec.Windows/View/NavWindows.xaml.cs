
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL;
using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL.Repositories.Abonnes;


namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour NavWindows.xaml
    /// </summary>
    public partial class NavWindows : Window
    {
        private readonly FilmService _filmService;
        private readonly AbonneService _abonneService;
        public NavWindows()
        {
            DatabaseGestion db = new DatabaseGestion();
            _filmService = new FilmService(new FilmRepository(db));
            _abonneService = new AbonneService(new AbonneRepsitory(db));

            InitializeComponent();
            mainContentControl.Content = new AdminHomeControl();

        }



        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new UsersControl(_abonneService);

        }

        private void ToggleButton_Films_Click(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new FilmsControl(_filmService);
        }


    }
}
