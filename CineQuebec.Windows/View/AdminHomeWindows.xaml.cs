
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL;
using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Projections;


namespace CineQuebec.Windows.View
{
    public partial class AdminHomeWindows : Window
    {
        private readonly FilmService _filmService;
        private readonly AbonneService _abonneService;
        private readonly AdminHomeControl _adminHomeControl;
        private readonly ProjectionService _projectionService;
        public string imageUrl { get; set; }
        public AdminHomeWindows(Abonne abonne)
        {
            DatabaseGestion db = new DatabaseGestion();
            _filmService = new FilmService(new FilmRepository(db));
            _abonneService = new AbonneService(new AbonneRepsitory(db));
            _projectionService = new ProjectionService(new ProjectionRepository(db));
            _adminHomeControl = new AdminHomeControl(abonne);
            InitializeComponent();
            DataContext = this;

            AdminName.Text = abonne.Username;
           imageUrl = $"https://robohash.org/{abonne.Id}";
          
            mainContentControl.Content = _adminHomeControl;

        }

        private void ToggleButton_Abonnes(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new UsersControl(_abonneService);

        }

        private void ToggleButton_Films_Click(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new AdminFilmsControl(_filmService, _projectionService);
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
    }
}
