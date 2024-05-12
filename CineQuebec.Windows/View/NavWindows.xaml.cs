
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL;
using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Projections;
using CineQuebec.Windows.BLL.Services.Connexion;
using CineQuebec.Windows.DAL.Repositories.Persons;
using CineQuebec.Windows.View.RecompenseAdminView;
using CineQuebec.Windows.BLL.Services.Recompenses;


namespace CineQuebec.Windows.View
{
    public partial class NavWindows : Window
    {
        private readonly FilmService _filmService;
        private readonly AbonneService _abonneService;
        private readonly AdminHomeControl _adminHomeControl;
        private readonly ProjectionService _projectionService;
        private readonly RecompenseService _recompenseService;
        private readonly ConnexionService _connexionService;
        public NavWindows(Admin admin)
        {
            DatabaseGestion db = new DatabaseGestion();
            _filmService = new FilmService(new FilmRepository(db));
            _abonneService = new AbonneService(new AbonneRepsitory(db));
            _projectionService = new ProjectionService(new ProjectionRepository(db));
            _adminHomeControl = new AdminHomeControl(admin);
            _connexionService = new ConnexionService(new PersonRepository(db));

            InitializeComponent();
            AdminName.Text = admin.Username;
            mainContentControl.Content = _adminHomeControl;

        }

        private void ToggleButton_Abonnes(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new UsersControl(_abonneService);

        }

        private void ToggleButton_Films_Click(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new FilmsControl(_filmService, _projectionService);
        }

        private void Button_Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Accueil_Click(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = _adminHomeControl;
        }

        private void ToggleButton_Recompense_Click(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new RecompenseView(_recompenseService);
        }
    }
}
