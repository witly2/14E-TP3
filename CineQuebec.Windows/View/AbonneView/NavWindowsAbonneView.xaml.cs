
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Projections;
using CineQuebec.Windows.BLL.Services.Connexion;
using CineQuebec.Windows.DAL.Repositories.Persons;
using CineQuebec.Windows.BLL.Services.Preferences;
using CineQuebec.Windows.DAL.Repositories.Preferences;


namespace CineQuebec.Windows.View.AbonneView
{
    public partial class NavWindowsAbonneView : Window
    {
        private readonly AbonneService _abonneService;
        private readonly AbonneHomeView _abonneHomeView;
        private readonly IPreferenceService _preferenceService;
        private readonly Abonne _abonne;
        private readonly AbonneProfilControl _abonneProfifilControl;
        public NavWindowsAbonneView(Abonne abonne)
        {
            DatabaseGestion db = new DatabaseGestion();
            _abonneService = new AbonneService(new AbonneRepsitory(db));
            _preferenceService = new PreferenceService(new PreferenceRepository(db));
            imageUrl = $"https://robohash.org/{abonne.Id}";
            InitializeComponent();
            // AbonneUsername.Text = abonne.Username;
            mainContentControl.Content = _abonneHomeView;
            _abonneProfifilControl = new AbonneProfilControl(abonne);
            _abonne = abonne;
        }
        public string imageUrl { get; set; }

        private void ToggleButton_Projections_Click(object sender, RoutedEventArgs e)
        {
            //mainContentControl.Content = new ProjectonView(_abonneService);

        }

        private void ToggleButton_Preferences_Click(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new PreferenceView(_abonne, _preferenceService);
        }

        private void Button_Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Accueil_Click(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = _abonneHomeView;
        }
        
        public void DetailFilmControl( Film film, Abonne abonne=null)
        {
            mainContentControl.Content = new DetailFilmControl(film,abonne);
        }

        private void btnAccueil(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

       

        private void btn_profilAbonne(object sender, MouseButtonEventArgs e)
        {
            mainContentControl.Content =_abonneProfifilControl;
        }
    }
}
