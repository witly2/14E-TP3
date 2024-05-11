using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.BLL.Services.Connexion;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL.Repositories.Persons;
using CineQuebec.Windows.DAL.Repositories.Projections;
using CineQuebec.Windows.View;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CineQuebec.Windows.View.AbonneView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowAbonne : Window
    {
        private readonly FilmService _filmService;
        private readonly AbonneService _abonneService;
        private readonly ProjectionService _projectionService;
        private readonly ConnexionService _connexionService;
        private readonly Film film;

        public MainWindowAbonne()
        {
            DatabaseGestion db = new DatabaseGestion();
            _filmService = new FilmService(new FilmRepository(db));
            _abonneService = new AbonneService(new AbonneRepsitory(db));
            _projectionService = new ProjectionService(new ProjectionRepository(db));

            InitializeComponent();

            mainContentControl.Content = new ConnexionControl(_connexionService);
        }

        public void PreferencesControl()
        {
            //mainContentControl.Content = new PreferenceControl();
        }

    }
}